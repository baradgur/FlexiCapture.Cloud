using System;
using System.Collections.Generic;
using System.IO;
using FlexiCapture.Cloud.EmailAttachmentService.Helpers.TaskHelpers;
using System.Xml.Serialization;
using FlexiCapture.Cloud.EmailAttachmentService.Models;
using FlexiCapture.Cloud.OCR.Assist.Models;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.ServiceAssist;
using FlexiCapture.Cloud.ServiceAssist.DB;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.EmailAttachmentService.Helpers.ProcessorHelper
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
                int serviceId = 4;// email attachment service id
                //getting available file extentions
                Assist assist = new Assist();
                Assist serviceAssist = new Assist();

                string serverPath = serviceAssist.GetSettingValueByName("MainPath");
                string uploadFolder = serviceAssist.GetSettingValueByName("UploadFolder");
                string uploadUrl = Path.Combine(serverPath, uploadFolder);

                List<string> extentions = assist.GetToAvailableFileExtensions();
                // getting IMAP setttings
                string path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/settings.xml");

                if (File.Exists(path))
                {
                    ServiceSettingsModel settingsModel;

                    XmlSerializer formatter = new XmlSerializer(typeof(ServiceSettingsModel));
                    ServiceSettingsModel model = new ServiceSettingsModel();
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        settingsModel = formatter.Deserialize(fs) as ServiceSettingsModel;
                    }

                    //check tasks
                    List<Tasks> notExecutedTasks = assist.GetToNotExecutedTasks(serviceId);
                    //upload files
                    foreach (var notExecutedTask in notExecutedTasks)
                    {
                        OcrRequestModel requestModel = JsonConvert.DeserializeObject<OcrRequestModel>(notExecutedTask.ProfileContent);
                        if (requestModel.InputFiles != null && requestModel.InputFiles.Count > 0)
                        {
                            string extension = Path.GetExtension(requestModel.InputFiles[0].Name);
                            if (extension != null && extension!=".zip" && extension != ".rar")
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

                    EmailHelper.CreateTasksFromEmails(settingsModel, assist, extentions, uploadUrl, uploadFolder, serviceId);



                }
                else
                {
                    throw new FileNotFoundException("File not found in " + path);
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
