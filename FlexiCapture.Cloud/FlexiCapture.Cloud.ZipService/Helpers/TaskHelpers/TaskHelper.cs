using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using FlexiCapture.Cloud.ServiceAssist;
using FlexiCapture.Cloud.ServiceAssist.DB;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.OCR.Assist;
using FlexiCapture.Cloud.OCR.Assist.Models;
using Newtonsoft.Json;
using LogHelper = FlexiCapture.Cloud.ServiceAssist.DBHelpers.LogHelper;
using Tasks = FlexiCapture.Cloud.ServiceAssist.DB.Tasks;

namespace FlexiCapture.Cloud.ZipService.Helpers.TaskHelpers
{
    public static class TaskHelper
    {
        /// <summary>
        /// execute task
        /// </summary>
        public static void ExecuteTask(ZipTasks task)
        {
            try
            {
                AssistProcessor assist = new AssistProcessor();
                Assist serviceAssist = new Assist();

                string url = serviceAssist.GetSettingValueByName("ApiUrl");
                string json = task.ProfileContent;
                string error = "";
                string response = assist.MakeOcr(url, json, ref error);
                if (string.IsNullOrEmpty(response))
                {
                    LogHelper.AddLog(error);
                    serviceAssist.AddErrorToZipDocuments(task.Id, error);
                    serviceAssist.UpdateZipDocumentStatesByZipTaskId(task.Id, 4);
                    serviceAssist.UpdateZipTaskState(task.Id, 4);
                    return;
                }

                OcrResponseModel model = new OcrResponseModel();
                serviceAssist.UpdateZipTaskReponseContent(task.Id, response);
                model = JsonConvert.DeserializeObject<OcrResponseModel>(response);

                if (model.Status.Equals("Submitted"))
                {
                    serviceAssist.UpdateZipTaskState(task.Id, 2);
                    serviceAssist.UpdateZipDocumentStatesByZipTaskId(task.Id, 2);

                }
                else
                {
                    string errorText = "";
                    foreach (var ocrError in model.Errors)
                    {
                        errorText += ocrError.ErrorName + ": " + ocrError.ErrorMessage;
                    }
                    serviceAssist.AddErrorToZipDocuments(task.Id, errorText);
                    serviceAssist.UpdateZipTaskState(task.Id, 4);
                    serviceAssist.UpdateZipDocumentStatesByZipTaskId(task.Id, 4);

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
        /// check zip task
        /// </summary>
        public static void CheckStateZipTask(ZipTasks task)
        {
            try
            {
                AssistProcessor assist = new AssistProcessor();
                Assist serviceAssist = new Assist();
                OcrResponseModel model = new OcrResponseModel();
                model = JsonConvert.DeserializeObject<OcrResponseModel>(task.ResponseContent);

                string jobStatus = assist.GetJobStatus(model.JobUrl);

                model = JsonConvert.DeserializeObject<OcrResponseModel>(jobStatus);

                if (model.Status.Equals("Finished"))
                {
                    string pathToDownload = serviceAssist.GetSettingValueByName("MainPath");
                    string resultZipFolder = serviceAssist.GetSettingValueByName("ResultZipFolder");
                    string jobPattern = serviceAssist.GetSettingValueByName("ApiUrlJobState");

                    string downloadPath = Path.Combine(pathToDownload, resultZipFolder);

                    foreach (var file in model.Download)
                    {
                        string jobId = model.JobUrl.Replace(jobPattern, string.Empty);
                        ZipDocuments zipDocument = serviceAssist.GetToZipDocumentByTaskId(task.Id);
                        if (zipDocument == null)
                        {
                            serviceAssist.UpdateTaskState(task.Id, 4);
                            return;
                        }
                        string originalName = Path.GetFileNameWithoutExtension(zipDocument.OriginalFileName);
                        string fileExt = serviceAssist.GetToFileExtension(file.OutputFormat);

                        Guid g = Guid.NewGuid();
                        string newName = g.ToString() + fileExt;

                        originalName = originalName + fileExt;
                        string filePath = Path.Combine(downloadPath, newName);
                        string error = "";
                        assist.DownloadFile(file.Uri, filePath, ref error);
                        if (!File.Exists(filePath))
                        {
                            LogHelper.AddLog(error);
                            serviceAssist.AddErrorToZipDocuments(task.Id, error);
                            //update task
                            serviceAssist.UpdateZipTaskState(task.Id, 4);
                            //update documents
                            serviceAssist.UpdateZipDocumentStatesByZipTaskId(task.Id, 4);
                            return;
                        }
                        //add document
                        serviceAssist.AddResultZipDocument(task.Id, g, originalName, newName, filePath);
                    }

                    //update task
                    serviceAssist.UpdateZipTaskState(task.Id, 3);
                    //update documents
                    serviceAssist.UpdateZipDocumentStatesByZipTaskId(task.Id, 3);
                }
                else if (model.Status.Equals("Processing"))
                {
                }
                else if (!model.Status.Equals("Submitted"))
                {
                    LogHelper.AddLog("Error in JobStatus: " + jobStatus);
                    string errorText = "";
                    foreach (var ocrError in model.Errors)
                    {
                        errorText += ocrError.ErrorName + ": " + ocrError.ErrorMessage;
                    }
                    serviceAssist.AddErrorToZipDocuments(task.Id, errorText);
                    //update task
                    serviceAssist.UpdateZipTaskState(task.Id, 4);
                    //update documents
                    serviceAssist.UpdateZipDocumentStatesByZipTaskId(task.Id, 4);
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

        public static void CheckStateTask(Tasks outerTask, Assist assist, string serverPath, string resultZipFolder, string resultFolder)
        {
            try
            {
                // check whether all zip tasks are completed
                bool hasSuccess = false;

                List<Tuple<int, string>> downloadIds = new List<Tuple<int, string>>();
                List<Tuple<string, string>> attachmentsLinks = new List<Tuple<string, string>>();
                if (outerTask.ServiceId == 4) //if email attachment service
                {
                    assist.EmailSettings = assist.GetToEmailConversionSettings(outerTask.UserId);
                    if (assist.EmailSettings == null || assist.EmailSettings.ResponseSettings == null)
                    {
                        return;
                    }
                }
                foreach (var zipTask in outerTask.ZipTasks)
                {
                    if (zipTask.TaskStateId == 1 || zipTask.TaskStateId == 2)
                    {
                        return;
                    }
                    if (zipTask.TaskStateId == 3)
                    {
                        hasSuccess = true;
                    }
                }
                // creating result document name and guid
                string originalArchiveName = "archive.zip";
                var originalDoc = outerTask.Documents.FirstOrDefault();
                if (originalDoc != null)
                {
                    originalArchiveName = Path.GetFileNameWithoutExtension(originalDoc.OriginalFileName)+"_result";
                }
                else
                { return; }
                string fileExt = ".zip";

                Guid archiveGuid = Guid.NewGuid();
                string newName = archiveGuid.ToString() + fileExt;

                originalArchiveName = originalArchiveName + fileExt;
                string filePath = Path.Combine(serverPath, resultFolder, newName);

                if (hasSuccess)
                {
                    // make archive
                    using (ZipArchive archive = ZipFile.Open(filePath, ZipArchiveMode.Create))
                    {
                        foreach (var zipTask in outerTask.ZipTasks)
                        {
                            foreach (var zipDoc in zipTask.ZipDocuments)
                            {
                                if (zipDoc.DocumentCategoryId == 2)
                                {
                                    archive.CreateEntryFromFile(Path.Combine(serverPath, zipDoc.Path), zipDoc.OriginalFileName);
                                }
                            }
                        }
                    }
                }
                // delete all unpacked files and result zip docs
                foreach (var zipTask in outerTask.ZipTasks)
                {
                    foreach (var zipDoc in zipTask.ZipDocuments)
                    {
                        File.Delete(Path.Combine(serverPath, zipDoc.Path));
                    }
                }
                // updating task and documents, adding result document
                assist.UpdateDocumentErrorsFromZipDocs(outerTask.Id);
                assist.UpdateTaskState(outerTask.Id, hasSuccess ? 3 : 4);
                assist.UpdateDocumentStatesByTaskId(outerTask.Id, hasSuccess ? 3 : 4);
                if (hasSuccess)
                {
                    int resultDocumentId = assist.AddResultDocument(outerTask.Id, archiveGuid, originalArchiveName,
                        newName, filePath);
                    if (assist.EmailSettings != null && assist.EmailSettings.ResponseSettings != null &&
                        assist.EmailSettings.ResponseSettings.AddAttachment)
                    {
                        attachmentsLinks.Add(new Tuple<string, string>(filePath, originalArchiveName));
                    }
                    if (assist.EmailSettings != null && assist.EmailSettings.ResponseSettings != null &&
                        assist.EmailSettings.ResponseSettings.AddLink)
                    {
                        downloadIds.Add(new Tuple<int, string>(resultDocumentId, originalArchiveName));
                    }
                }
                if (outerTask.ServiceId == 4) //if email attachment service
                {

                    if (assist.EmailSettings != null && assist.EmailSettings.ResponseSettings != null && assist.EmailSettings.ResponseSettings.SendReply)
                    {
                        if (hasSuccess)
                        {
                            string text =
                                "DataCapture.Cloud received a conversion request form this e-mail address.  Here is your conversion result:";
                            assist.SendEmailResponse(outerTask.UserId, downloadIds, attachmentsLinks,
                                assist.EmailSettings.ResponseSettings.CCResponse
                                    ? assist.EmailSettings.ResponseSettings.Addresses
                                    : "",
                                text);
                        }
                        else
                        {
                            assist.SendEmailResponseFail(outerTask.UserId, "DataCapture.Cloud received a conversion request form this e - mail address. Error occured while processing request.", "");
                        }
                    }
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
