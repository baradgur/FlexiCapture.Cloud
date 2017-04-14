using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.Helpers;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
{
    public class DocumentsHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetToDocumentFileType(string extension)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    List<DocumentTypes> types = db.DocumentTypes.Select(x => x).ToList();
                    foreach (var type in types)
                    {
                        List<string> elements = type.Extension.Split(';').ToList();

                        foreach (var element in elements)
                        {
                            if (!string.IsNullOrEmpty(element))
                            {
                                if (element.Equals(extension.ToLower())) return type.Id;
                            }
                        }
                    }
                }
                return -1;
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
        /// update document state
        /// </summary>
        public static void UpdateDocumentState(int documentId, int stateId)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    Documents document = db.Documents.FirstOrDefault(x => x.Id == documentId);
                    if (document != null)
                        document.DocumentStateId = stateId;
                    db.SaveChanges();
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

        /// <summary>
        /// get to documents by task id
        /// </summary>
        /// <returns></returns>
        public static List<Documents> GetToDocumentsByTaskId(int taskId)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    return db.Documents.Where(x => x.TaskId == taskId).ToList();
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return null;
            }
        }

        /// <summary>
        /// update documents in task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="stateId"></param>
        public static void UpdateDocumentStatesByTaskId(int taskId, int stateId)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    List<Documents> documents = db.Documents.Where(x => x.TaskId == taskId).ToList();
                    foreach (Documents document in documents)
                    {
                        UpdateDocumentState(document.Id,stateId);
                    }
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

        /// <summary>
        /// get to single document
        /// </summary>
        /// <param name="taskId"></param>
        public static Documents GetToDocumentByTaskId(int taskId)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    return db.Documents.FirstOrDefault(x => x.TaskId == taskId && x.DocumentCategoryId == 1);
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return null;
            }
        }
        
        /// <summary>
        /// add result document to db
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="guid"></param>
        /// <param name="originalFileName"></param>
        public static void AddResultDocument(int taskId, Guid guid, string originalFileName,string realFileName,string filePath)
        {
            try
            {
                ServiceAssist.Assist assist = new Assist();
                string resultPath = assist.GetSettingValueByName("ResultFolder");
                string path = Path.Combine(resultPath, realFileName);
                FileInfo info = new FileInfo(filePath);
                using (var db = new FCCPortalEntities())
                {
                    Documents document = new Documents();
                    document.Date = DateTime.Now;
                    document.TaskId = taskId;
                    document.DocumentCategoryId = 2;
                    document.OriginalFileName = originalFileName;
                    document.DocumentStateId = 3;
                    document.DocumentTypeId = GetToDocumentFileType(Path.GetExtension(originalFileName));
                    document.Guid = guid;
                    document.FileSize = info.Length;
                    document.Hash = MD5Helper.GetMD5HashFromFile(filePath);
                    document.FileName = realFileName;
                    document.Path = path;
                    db.Documents.Add(document);
                    db.SaveChanges();
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
