using System;
using System.Collections.Generic;
using System.IO;
using FlexiCapture.Cloud.OCR.Assist.Models;
using FlexiCapture.Cloud.ServiceAssist;
using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.DBHelpers;
using FlexiCapture.Cloud.SingleFileConversionService.Helpers.TasksHelpers;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.SingleFileConversionService.Helpers.ProcessorHelpers
{
    /// <summary>
    /// helper for process service
    /// </summary>
    public static class ProcessorHelper
    {
        /// <summary>
        /// make processing 
        /// </summary>
        public static void MakeProcessing()
        {
            try
            {
                int serviceId = 1;
                ServiceAssist.Assist assist = new Assist();
                
                //check tasks
                List<Tasks> notExecutedTasks = assist.GetToNotExecutedTasks(serviceId);
                //upload files
                foreach (var notExecutedTask in notExecutedTasks)
                {
                    OcrRequestModel requestModel = JsonConvert.DeserializeObject<OcrRequestModel>(notExecutedTask.ProfileContent);
                    if (requestModel.InputFiles != null && requestModel.InputFiles.Count > 0)
                    {
                        string extension = Path.GetExtension(requestModel.InputFiles[0].Name);
                        if (extension != null && extension != ".zip" && extension != ".rar" && extension != ".7z")
                        {
                            TaskHelper.ExecuteTask(notExecutedTask);
                        }
                    }
                }

                //check statuses
                List<Tasks> processedTasks = assist.GetToProcessedTasks(serviceId);
                //download files
                foreach (var processedTask in processedTasks)
                {
                    TaskHelper.CheckStateTask(processedTask);
                }
                //update states
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
