using System;
using System.Collections.Generic;
using System.IO;
using FlexiCapture.Cloud.ServiceAssist;
using System.IO.Compression;
using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.DBHelpers;

namespace FlexiCapture.Cloud.ZipService.Helpers
{
    public static class ZipHelper
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

        public static void CreateZipTasksFromTasks(Assist assist, List<string> extentions, Tasks task, string uploadUrl, string uploadFolder, string uploadZipUrl, string inputZipPath)
        {
            try
            {
                string url = assist.GetSettingValueByName("ApiUrl");
                LogHelper.AddLog("ZIP Path="+inputZipPath);
                using (ZipArchive archive = ZipFile.OpenRead(inputZipPath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        string entryExtention = Path.GetExtension(entry.FullName);
                        if (CheckExtensions(extentions, entryExtention))
                        {
                            
                            var newNameGuid = Guid.NewGuid();
                            var uploadName = newNameGuid + entryExtention;

                            string uZipUrl = SettingsHelper.GetSettingValueByName("UploadZipFolder");
                            string uZipFilePath = Path.Combine(uZipUrl, uploadName);
                            var filePath = Path.Combine(uploadZipUrl, uploadName);
                            string originalFileName = Path.GetFileName(entry.FullName);
                            entry.ExtractToFile(filePath);
                            assist.AddLog("Unzipped file: " + uploadName);
                            //add task to db
                            var taskId = assist.AddZipTask(assist.UserProfile.UserId, task.ServiceId, task.Id);
                            assist.AddLog("Add task: " + taskId);

                            var md5 = assist.GetMD5HashFromFile(filePath);
                            //add document
                            var fileInfo = new FileInfo(filePath);

                            var documentId = assist.AddZipDocument(taskId, fileInfo, originalFileName, newNameGuid, uploadName, uZipFilePath, md5, 1);

                            assist.ZipDocuments = assist.GetZipDocumentsByZipTaskId(taskId);

                            string content = assist.ConvertProfileToRequestModel(assist.ZipDocuments, assist.UserProfile);
                            assist.UpdateZipTaskProfile(taskId, content);
                        }
                    }
                    assist.UpdateDocumentStatesByTaskId(task.Id, 2);
                    assist.UpdateTaskState(task.Id, 2);
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

        #endregion
    }
}
