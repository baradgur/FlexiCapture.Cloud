using System;
using System.IO;
using FlexiCapture.Cloud.OCR.Assist;
using FlexiCapture.Cloud.OCR.Assist.Models;
using FlexiCapture.Cloud.ServiceAssist;
using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.DBHelpers;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.BatchFileConversionService.Helpers.TasksHelpers
{
    public static class TaskHelper
    {
        /// <summary>
        /// execute task
        /// </summary>
        public static void ExecuteTask(Tasks task)
        {
            try
            {
                AssistProcessor assist = new AssistProcessor();
                Assist serviceAssist = new Assist();

                string url = serviceAssist.GetSettingValueByName("ApiUrl");
                string json = task.ProfileContent;
                string error = "";
                string response =assist.MakeOcr(url,json,ref error);
                if (string.IsNullOrEmpty(response))
                {
                    LogHelper.AddLog(error);
                    serviceAssist.UpdateDocumentStatesByTaskId(task.Id, 4);
                    serviceAssist.UpdateTaskState(task.Id,4);
                    return;
                }

                OcrResponseModel model = new OcrResponseModel();
                serviceAssist.UpdateTaskReponseContent(task.Id,response);
                model =  JsonConvert.DeserializeObject<OcrResponseModel>(response);

                if (model.Status.Equals("Submitted"))
                {
                    serviceAssist.UpdateTaskState(task.Id, 2);
                    serviceAssist.UpdateDocumentStatesByTaskId(task.Id,2);
                    
                }
                else
                {
                    serviceAssist.UpdateTaskState(task.Id,4);
                    serviceAssist.UpdateDocumentStatesByTaskId(task.Id, 4);

                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
            }
        }

        /// <summary>
        /// check task 
        /// </summary>
        public static void CheckStateTask(Tasks task)
        {
            try
            {
                AssistProcessor assist = new AssistProcessor();
                Assist serviceAssist = new Assist();
                OcrResponseModel model = new OcrResponseModel();
                model = JsonConvert.DeserializeObject<OcrResponseModel>(task.ResponseContent);

                string jobStatus = assist.GetJobStatus(model.JobUrl);

                model= JsonConvert.DeserializeObject<OcrResponseModel>(jobStatus);

                if (model.Status.Equals("Finished"))
                {
                    string pathToDownload = serviceAssist.GetSettingValueByName("MainPath");
                    string resultFolder = serviceAssist.GetSettingValueByName("ResultFolder");
                    string jobPattern = serviceAssist.GetSettingValueByName("ApiUrlJobState");

                    string downloadPath = Path.Combine(pathToDownload, resultFolder);

                    foreach (var file in model.Download)
                    {
                        string jobId = model.JobUrl.Replace(jobPattern, string.Empty);
                        Documents document = serviceAssist.GetToDocumentByTaskId(task.Id);
                        if (document == null)
                        {
                            serviceAssist.UpdateTaskState(task.Id,4);
                            return;
                        }
                        string originalName = Path.GetFileNameWithoutExtension(document.OriginalFileName);
                        string fileExt = serviceAssist.GetToFileExtension(file.OutputFormat);

                        Guid g = Guid.NewGuid();
                        string newName = g.ToString() + fileExt;

                        originalName = originalName + fileExt;
                        string filePath = Path.Combine(downloadPath,newName);
                        string error = "";
                        assist.DownloadFile(file.Uri,filePath,ref error);
                        if (!File.Exists(filePath))
                        {
                            LogHelper.AddLog(error);
                            //update task
                            serviceAssist.UpdateTaskState(task.Id, 4);
                            //update documents
                            serviceAssist.UpdateDocumentStatesByTaskId(task.Id, 4);
                            return;
                        }
                        //add document
                        serviceAssist.AddResultDocument(task.Id,g,originalName,newName,filePath);
                    }

                    //update task
                    serviceAssist.UpdateTaskState(task.Id,3);
                    //update documents
                    serviceAssist.UpdateDocumentStatesByTaskId(task.Id,3);
                }
                else if (!model.Status.Equals("Submitted"))
                {
                    LogHelper.AddLog("Error in JobStatus: "+jobStatus);
                    //update task
                    serviceAssist.UpdateTaskState(task.Id, 4);
                    //update documents
                    serviceAssist.UpdateDocumentStatesByTaskId(task.Id, 4);
                }

            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);   
            }
        }
    }
}
