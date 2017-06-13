using System;
using System.Collections.Generic;
using System.IO;
using FlexiCapture.Cloud.ZipService.Helpers.TaskHelpers;
using System.Xml.Serialization;
using FlexiCapture.Cloud.OCR.Assist.Models;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.ServiceAssist;
using FlexiCapture.Cloud.ServiceAssist.DB;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.ZipService.Helpers.ProcessorHelper
{
    public static class ProcessorHelper
    {
        /// <summary>
        /// make processing 
        /// </summary>
        public static void MakeProcessing()
        {
            try
            {
                //getting available file extentions
                Assist serviceAssist = new Assist();

                string serverPath = serviceAssist.GetSettingValueByName("MainPath");
                string uploadFolder = serviceAssist.GetSettingValueByName("UploadFolder");
                string resultFolder = serviceAssist.GetSettingValueByName("ResultFolder");
                string resultZipFolder = serviceAssist.GetSettingValueByName("ResultZipFolder");
                string uploadZipFolder = serviceAssist.GetSettingValueByName("UploadZipFolder");
                string uploadUrl = Path.Combine(serverPath, uploadFolder);
                string uploadZipUrl = Path.Combine(serverPath, uploadZipFolder);

                List<string> extentions = serviceAssist.GetToAvailableFileExtensions();
                
                //check tasks
                List<Tasks> notExecutedTasks = serviceAssist.GetToNotExecutedTasks();
                //upload files
                foreach (var notExecutedTask in notExecutedTasks)
                {
                    serviceAssist.UserProfile = serviceAssist.GetUserProfile(notExecutedTask.UserId, notExecutedTask.ServiceId);
                    Documents document = serviceAssist.GetToDocumentByTaskId(notExecutedTask.Id);
                    if (document != null)
                    {
                        string extension = Path.GetExtension(document.OriginalFileName);
                        if (extension != null && (extension == ".zip" || extension == ".rar"))
                        {
                            string inputPath = Path.Combine(serverPath, document.Path);
                            ZipHelper.CreateZipTasksFromTasks(serviceAssist, extentions, notExecutedTask, uploadUrl,
                                uploadFolder, uploadZipUrl, inputPath);
                        }
                    }
                    else
                    {
                        serviceAssist.UpdateTaskState(notExecutedTask.Id, 4);
                    }
                }

                //check zipTasks
                List<ZipTasks> notExecutedZipTasks = serviceAssist.GetToNotExecutedZipTasks();
                //upload files
                foreach (var notExecutedTask in notExecutedZipTasks)
                {
                    TaskHelper.ExecuteTask(notExecutedTask);
                }
                //check statuses of Zip tasks
                List<ZipTasks> processedZipTasks = serviceAssist.GetToProcessedZipTasks();
                //download files
                foreach (var processedZipTask in processedZipTasks)
                {
                    TaskHelper.CheckStateZipTask(processedZipTask);
                }

                List<Tasks> outerTasks = serviceAssist.GetToOuterTasks();

                foreach (var outerTask in outerTasks)
                {
                    TaskHelper.CheckStateTask(outerTask, serviceAssist, serverPath, resultZipFolder, resultFolder);
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
