﻿using System;
using System.Collections.Generic;
using System.IO;
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
        /// check extesoion method
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        private static bool CheckExtensions(List<string> extensions, string ext)
        {
            try
            {
                foreach (var extension in extensions)
                {
                    if (extension.ToLower().Contains(ext.ToLower())) return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// make processing 
        /// </summary>
        public static void MakeProcessing()
        {
            try
            {
                int serviceId = 3;
                ServiceAssist.Assist assist = new Assist();
                List<string> extensions = assist.GetToAvailableFileExtensions();
                
                List<FtpWebResponse> responses = new List<FtpWebResponse>();
                
                FTPHelper.GetFtpSettings().ForEach(x =>
                {

                    var response = FTPHelper.TryLoginToFtp(x.Host, x.UserName,
                        PasswordHelper.Crypt.DecryptString(x.Password), x.UserId);

                    List<Tuple<string, string>> addedFilesInResponse;

                    if (response != null)
                    {
                        assist.UserProfile = assist.GetUserProfile(x.UserId, 3);

                        addedFilesInResponse = FTPHelper.ExtractFiles(response, x.Host, x.UserName,
                            PasswordHelper.Crypt.DecryptString(x.Password));

                        addedFilesInResponse.ForEach(af =>
                        {

                            if (CheckExtensions(extensions, af.Item2))
                            {


                                var newNameGuid = Guid.NewGuid();
                                var uploadName = newNameGuid + af.Item2;
                                var localName = Path.Combine(Path.Combine("data", "uploads"), uploadName);

                                string originalFileName = af.Item1;
                                var filePathOld = Path.Combine(assist.GetSettingValueByName("MainPath"), "data",
                                    "uploads", af.Item1);
                                var filePathNew = Path.Combine(assist.GetSettingValueByName("MainPath"), "data",
                                    "uploads", newNameGuid.ToString() + af.Item2);



                                //add task to db
                                var taskId = assist.AddTask(assist.UserProfile.UserId, serviceId);

                                var md5 = assist.GetMD5HashFromFile(filePathOld);
                                //add document
                                var fileInfo = new FileInfo(filePathOld);


                                var documentId = assist.AddDocument(taskId, fileInfo, originalFileName, newNameGuid,
                                    uploadName, localName, md5, 1);

                                System.IO.File.Move(filePathOld, filePathNew);
                                if (File.Exists(filePathOld))
                                    File.Delete(filePathOld);

                                assist.Documents = assist.GetDocumentsByTaskId(taskId);

                                string content = assist.ConvertProfileToRequestModel(assist.Documents,
                                    assist.UserProfile);
                                assist.UpdateTaskProfile(taskId, content);
                            }
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

