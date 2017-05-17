using System;
using System.Collections.Generic;
using System.IO;
using FlexiCapture.Cloud.EmailAttachmentService.Models;
using FlexiCapture.Cloud.ServiceAssist;
using ImapX;
using ImapX.Enums;

namespace FlexiCapture.Cloud.EmailAttachmentService.Helpers
{
    public static class EmailHelper
    {
        #region imap helpers

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
        /// receive emails from IMAP
        /// </summary>
        public static void CreateTasksFromEmails(ServiceSettingsModel model, Assist assist, List<string> extensions, string uploadPath, string uploadFolder, int serviceId)
        {
            try
            {
                var client = new ImapClient(model.ImapSettings.Server, model.ImapSettings.Port, model.ImapSettings.UseSSL);
                if (client.Connect())
                {

                    if (client.Login(model.Credentials.UserName, model.Credentials.Password))
                    {
                        // login successful

                        List<string> lst = new List<string>();

                        foreach (var folder in client.Folders)
                        {
                            lst.Add(folder.Name);

                            if (folder.Name.ToLower().Equals(model.ImapSettings.DefaultFolder.ToLower()))
                            {
                                folder.Messages.Download("ALL", MessageFetchMode.Full, Int32.MaxValue);
                                //if (folder.Unseen>0)
                                foreach (var message in folder.Messages)
                                {
                                    if (!message.Seen)
                                    {
                                        assist.AddLog("Check not seen letter from: " + message.From);
                                   
                                        // check whether this email is active and user has email service
                                        assist.UserProfile = assist.CheckServiceAvailabilityByEmail(message.From.Address);
                                        if (assist.UserProfile != null)
                                        {

                                            if (message.Body != null)
                                            {
                                                //change  user profile
                                            }

                                            foreach (var attachment in message.Attachments)
                                            {
                                                var extension = Path.GetExtension(attachment.FileName);
                                                if (CheckExtensions(extensions,extension))// check whether file extension is in the extensions list
                                                {
                                                    var newNameGuid = Guid.NewGuid();
                                                    var uploadName = newNameGuid + extension;
                                                    var localName = Path.Combine(uploadFolder, uploadName);
                                                    var filePath = Path.Combine(uploadPath, uploadName);
                                                    string originalFileName = attachment.FileName;
                                                    attachment.Download();
                                                    attachment.Save(uploadPath, uploadName);
                                                    assist.AddLog("Download file: "+uploadName);
                                                    //add task to db
                                                    var taskId = assist.AddTask(assist.UserProfile.UserId, serviceId);
                                                    assist.AddLog("Add task: " + taskId);

                                                    var md5 = assist.GetMD5HashFromFile(filePath);
                                                    //add document
                                                    var fileInfo = new FileInfo(filePath);

                                                    var documentId = assist.AddDocument(taskId, fileInfo, originalFileName, newNameGuid, uploadName, localName, md5, 1);

                                                    assist.Documents = assist.GetDocumentsByTaskId(taskId);

                                                    string content =assist.ConvertProfileToRequestModel(assist.Documents, assist.UserProfile);
                                                    assist.UpdateTaskProfile(taskId, content);
                                                }
                                            }

                                        }
                                        message.Seen = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    assist.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);

            }
        }
        #endregion
    }
}
