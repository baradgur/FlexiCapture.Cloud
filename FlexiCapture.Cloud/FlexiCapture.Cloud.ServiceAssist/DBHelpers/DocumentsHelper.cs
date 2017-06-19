using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.Helpers;
using System.Data.Entity;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.ServiceAssist.Models.Documents;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
{
    public class DocumentsHelper
    {
        /// <summary>
        /// update document state
        /// </summary>
        public static void UpdateDocumentState(int documentId, int stateId)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
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

        public static void AddErrorToDocuments(int taskId, string error)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                using (var db = new FCCPortalEntities2())
                {
                    List<Documents> documents = db.Documents.Where(x => x.TaskId == taskId).ToList();
                    foreach (Documents document in documents)
                    {

                        List<DocumentError> errors = serializer.Deserialize<List<DocumentError>>(document.ErrorText??"");
                        if (errors == null)
                        {
                            errors = new List<DocumentError>();
                        }
                        errors.Add(new DocumentError()
                        {
                            DocumentName = document.OriginalFileName,
                            ErrorText = error
                        });
                        document.ErrorText = serializer.Serialize(errors);
                    }
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
        /// update zipDocuments state by task Id
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="stateId"></param>
        public static void UpdateZipDocumentStatesByZipTaskId(int taskId, int stateId)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    List<ZipDocuments> documents = db.ZipDocuments.Where(x => x.TaskId == taskId).ToList();
                    foreach (ZipDocuments document in documents)
                    {
                        document.DocumentStateId = stateId;
                    }
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
                using (var db = new FCCPortalEntities2())
                {
                    return db.Documents
                        .Include(x=>x.DocumentTypes)
                        .Where(x => x.TaskId == taskId).ToList();
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

        public static int AddZipDocument(int taskId, FileInfo file, string originalFileName, Guid guid, string gFileName, string path, string md5, int categoryId)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    ZipDocuments document = new ZipDocuments()
                    {
                        Date = DateTime.Now,
                        DocumentStateId = 1,
                        DocumentTypeId = DocumentTypesHelper.GetToDocumentFileType(file.Extension),
                        FileName = gFileName,
                        FileSize = file.Length,
                        Hash = md5,
                        Guid = guid,
                        Path = path,
                        OriginalFileName = originalFileName,
                        TaskId = taskId,
                        DocumentCategoryId = categoryId


                    };
                    db.ZipDocuments.Add(document);
                    db.SaveChanges();
                    return document.Id;
                }

                return -1;
            }
            catch (Exception exception)
            {
                return -1;
            }
        }

        public static void AddErrorToZipDocuments(int taskId, string error)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                using (var db = new FCCPortalEntities2())
                {
                    List<ZipDocuments> documents = db.ZipDocuments.Where(x => x.TaskId == taskId).ToList();
                    foreach (ZipDocuments document in documents)
                    {

                        List<DocumentError> errors = serializer.Deserialize<List<DocumentError>>(document.ErrorText ?? "");
                        if (errors == null)
                        {
                            errors = new List<DocumentError>();
                        }
                        errors.Add(new DocumentError()
                        {
                            DocumentName = document.OriginalFileName,
                            ErrorText = error
                        });
                        document.ErrorText = serializer.Serialize(errors);
                    }
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

        public static ZipDocuments GetToZipDocumentByTaskId(int taskId)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    return db.ZipDocuments.FirstOrDefault(x => x.TaskId == taskId && x.DocumentCategoryId == 1);
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

        public static List<ZipDocuments> GetToZipDocumentsByZipTaskId(int taskId)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    return db.ZipDocuments
                        .Include(x => x.DocumentTypes)
                        .Where(x => x.TaskId == taskId).ToList();
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

        public static void UpdateDocumentErrorsFromZipDocs(int outerTaskId)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<DocumentError> errors = new List<DocumentError>();
                using (var db = new FCCPortalEntities2())
                {
                    var task = db.Tasks
                        .Include(x=>x.Documents)
                        .Include(x=>x.ZipTasks.Select(xx=>xx.ZipDocuments))
                        .FirstOrDefault(x => x.Id == outerTaskId);
                    if (task == null)
                    {
                        return;
                    }

                    foreach (var zipTask in task.ZipTasks)
                    {
                        foreach (var zipDoc in zipTask.ZipDocuments)
                        {
                            List<DocumentError> zipErrors = serializer.Deserialize<List<DocumentError>>(zipDoc.ErrorText??"");
                            if (zipErrors != null) { 
                            errors.AddRange(zipErrors);
                            }
                        }
                    }

                    foreach (Documents document in task.Documents)
                    {
                        document.ErrorText = serializer.Serialize(errors);
                    }
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
        /// update documents in task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="stateId"></param>
        public static void UpdateDocumentStatesByTaskId(int taskId, int stateId)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
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
                using (var db = new FCCPortalEntities2())
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

        public static int AddDocument(int taskId, FileInfo file, string originalFileName, Guid guid, string gFileName, string path, string md5, int categoryId)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    Documents document = new Documents()
                    {
                        Date = DateTime.Now,
                        DocumentStateId = 1,
                        DocumentTypeId = DocumentTypesHelper.GetToDocumentFileType(file.Extension),
                        FileName = gFileName,
                        FileSize = file.Length,
                        Hash = md5,
                        Guid = guid,
                        Path = path,
                        OriginalFileName = originalFileName,
                        TaskId = taskId,
                        DocumentCategoryId = categoryId


                    };
                    db.Documents.Add(document);
                    db.SaveChanges();
                    return document.Id;
                }

                return -1;
            }
            catch (Exception exception)
            {
                return -1;
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
                using (var db = new FCCPortalEntities2())
                {
                    Documents document = new Documents();
                    document.Date = DateTime.Now;
                    document.TaskId = taskId;
                    document.DocumentCategoryId = 2;
                    document.OriginalFileName = originalFileName;
                    document.DocumentStateId = 3;
                    document.DocumentTypeId = DocumentTypesHelper.GetToDocumentFileType(Path.GetExtension(originalFileName));
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

        public static void AddResultZipDocument(int taskId, Guid guid, string originalFileName, string realFileName, string filePath)
        {
            try
            {
                ServiceAssist.Assist assist = new Assist();
                string resultPath = assist.GetSettingValueByName("ResultZipFolder");
                string path = Path.Combine(resultPath, realFileName);
                FileInfo info = new FileInfo(filePath);
                using (var db = new FCCPortalEntities2())
                {
                    ZipDocuments document = new ZipDocuments();
                    document.Date = DateTime.Now;
                    document.TaskId = taskId;
                    document.DocumentCategoryId = 2;// means output document
                    document.OriginalFileName = originalFileName;
                    document.DocumentStateId = 3;
                    document.DocumentTypeId = DocumentTypesHelper.GetToDocumentFileType(Path.GetExtension(originalFileName));
                    document.Guid = guid;
                    document.FileSize = info.Length;
                    document.Hash = MD5Helper.GetMD5HashFromFile(filePath);
                    document.FileName = realFileName;
                    document.Path = filePath;
                    db.ZipDocuments.Add(document);
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
