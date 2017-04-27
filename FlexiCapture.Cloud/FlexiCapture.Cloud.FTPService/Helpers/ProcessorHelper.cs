using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.FTPService.Helpers.TasksHelpers;
using FlexiCapture.Cloud.FTPService.Models;
using FlexiCapture.Cloud.Portal.Api.Helpers.CryptHelpers;
using FlexiCapture.Cloud.ServiceAssist;
using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.DBHelpers;

namespace FlexiCapture.Cloud.FTPService.Helpers
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
                int serviceId = 3;
                ServiceAssist.Assist assist = new Assist();

                List<FtpWebResponse> responses = new List<FtpWebResponse>();
                
                FTPHelper.GetFtpSettings().ForEach(x =>
                {
                    var response = FTPHelper.TryLoginToFtp(x.Host, x.UserName,
                        PasswordHelper.Crypt.DecryptString(x.Password), x.UserId);

                    List<Tuple<string, string>> addedFilesInResponse;

                    if (response != null)
                    {
                        addedFilesInResponse = FTPHelper.ExtractFiles(response, x.Host, x.UserName,
                            PasswordHelper.Crypt.DecryptString(x.Password));

                        addedFilesInResponse.ForEach(af =>
                        {
                            assist.AddTask(x.UserId, 3);
                        });
                    }


                });

                //responses.ForEach(x =>
                //{
                //    //assist.AddTask()
                //});

                //check tasks
                List<Tasks> notExecutedTasks = assist.GetToNotExecutedTasks(serviceId);
                //upload files
                foreach (var notExecutedTask in notExecutedTasks)
                {
                    TaskHelper.ExecuteTask(notExecutedTask);
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

