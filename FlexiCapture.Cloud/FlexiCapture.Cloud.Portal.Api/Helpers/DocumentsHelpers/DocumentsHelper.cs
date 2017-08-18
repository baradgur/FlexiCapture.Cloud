using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.CryptHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.RequestHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.Documents;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;
using Microsoft.Ajax.Utilities;
using Microsoft.Win32;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.DocumentsHelpers
{
    /// <summary>
    ///     documents helper
    /// </summary>
    public static class DocumentsHelper
    {
        /// <summary>
        /// add task to db
        /// </summary>
        public static int AddTask(int userId, int serviceId)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    Tasks task = new Tasks()
                    {
                        CreationDate = DateTime.Now,
                        TaskStateId = 1,
                        UserId = userId,
                        ServiceId = serviceId
                    };
                    db.Tasks.Add(task);
                    db.SaveChanges();
                    return task.Id;
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return -1;
            }
        }
        /// <summary>
        ///     process files
        /// </summary>
        public static bool ProcessFile(int userId, int serviceId, ManageUserProfileModel profile, HttpPostedFile file, string s)
        {
            try
            {
                var serverPath = "";
                var appFolder = "";
//#if (DEBUG)
                serverPath = HostingEnvironment.MapPath("~/");
               // appFolder = SettingsHelper.GetSettingsValueByName("appName");
//#else
//                serverPath = Environm 
//#endif
//                serverPath =ControllerContext.HttpContext.Server.MapPath
                //appFolder = SettingsHelper.GetSettingsValueByName("appName");
                var uploadFolder = SettingsHelper.GetSettingsValueByName("uploadFolder");
                var uploadPath = Path.Combine(serverPath, uploadFolder);

                var originalName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var newNameGuid = Guid.NewGuid();
                var uploadName = newNameGuid + extension;
                var localName = Path.Combine(uploadFolder, uploadName);
                uploadPath = Path.Combine(uploadPath, uploadName);
                file.SaveAs(uploadPath);
                //add task to db
                var taskId = TasksHelper.AddTask(userId, serviceId);
                var md5 = MD5Helper.GetMD5HashFromFile(uploadPath);
                //add document
                var documentId = DBHelpers.DocumentsHelper.AddDocument(taskId, file.FileName, file.ContentLength, newNameGuid, uploadName, localName,
                    md5,1);

                List<Documents> documents = DBHelpers.DocumentsHelper.GetDocumentsByTaskId(taskId);
                string content = ProfileToRequestModelConverter.ConvertProfileToRequestModel(documents, profile);
                TasksHelper.UpdateTaskProfile(taskId, content);

                return true;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return false;
            }
        }

        public static bool ProcessUrl(int userId, int serviceId, ManageUserProfileModel profile, string sPastedUrl, string s)
        {
            try
            {
                var serverPath = "";
                var appFolder = "";
                //#if (DEBUG)
                serverPath = HostingEnvironment.MapPath("~/");
                // appFolder = SettingsHelper.GetSettingsValueByName("appName");
                //#else
                //                serverPath = Environm 
                //#endif
                //                serverPath =ControllerContext.HttpContext.Server.MapPath
                //appFolder = SettingsHelper.GetSettingsValueByName("appName");
                var uploadFolder = SettingsHelper.GetSettingsValueByName("uploadFolder");
                var uploadPath = Path.Combine(serverPath, uploadFolder);

                Uri uri = new Uri(sPastedUrl);
                string filename = Path.GetFileName(uri.LocalPath);

                var originalName = Path.GetFileNameWithoutExtension(filename);
                var extension = Path.GetExtension(filename);

                if (extension.IsNullOrWhiteSpace())
                {
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(sPastedUrl);
                    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                    RegistryKey key;
                    object value;
                    key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + resp.ContentType, false);
                    value = key != null ? key.GetValue("Extension", null) : null;
                    extension = value != null ? value.ToString() : string.Empty;
                    filename += extension;
                }

                var newNameGuid = Guid.NewGuid();
                var uploadName = newNameGuid + extension;
                var localName = Path.Combine(uploadFolder, uploadName);
                uploadPath = Path.Combine(uploadPath, uploadName);

                using (var wbClient = new WebClient())
                {
                    wbClient.DownloadFile(sPastedUrl, uploadPath);
                }
                double contentLength =new System.IO.FileInfo(uploadPath).Length;
                //add task to db
                var taskId = TasksHelper.AddTask(userId, serviceId);
                var md5 = MD5Helper.GetMD5HashFromFile(uploadPath);
                //add document
                var documentId = DBHelpers.DocumentsHelper.AddDocument(taskId, filename, contentLength, newNameGuid, uploadName, localName,
                    md5, 1);
                

                List<Documents> documents = DBHelpers.DocumentsHelper.GetDocumentsByTaskId(taskId);
                string content = ProfileToRequestModelConverter.ConvertProfileToRequestModel(documents, profile);
                TasksHelper.UpdateTaskProfile(taskId, content);

                return true;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return false;
            }
        }
    }
}