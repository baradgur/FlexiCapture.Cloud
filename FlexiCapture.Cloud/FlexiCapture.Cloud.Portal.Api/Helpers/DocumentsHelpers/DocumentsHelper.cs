using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.CryptHelpers;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.DocumentsHelpers
{
    /// <summary>
    ///     documents helper
    /// </summary>
    public static class DocumentsHelper
    {
        /// <summary>
        ///     process files
        /// </summary>
        public static bool ProcessFile(int userId, int serviceId, HttpPostedFile file)
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
                    md5);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}