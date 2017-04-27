using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Hosting;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.CryptHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.RequestHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;

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
                using (var db = new FCCPortalEntities2())
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
                var documentId = DBHelpers.DocumentsHelper.AddDocument(taskId, file, newNameGuid, uploadName, localName,
                    md5,1);

                List<Documents> documents = DBHelpers.DocumentsHelper.GetDocumentsByTaskId(taskId);
                string content = ProfileToRequestModelConverter.ConvertProfileToRequestModel(documents, profile);
                TasksHelper.UpdateTaskProfile(taskId, content);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}