using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;
using System.Data.Entity;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.Models.Documents;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    /// <summary>
    /// documents helper
    /// </summary>
    public class DocumentsHelper
    {
        /// <summary>
        /// add document to database
        /// </summary>
        /// <returns></returns>
        public static int AddDocument(int taskId, string filename, double contentLength, Guid guid, string gFileName,string path, string md5, int categoryId)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                using (var db = new FCCPortalEntities())
                {
                    Documents document = new Documents()
                    {
                        Date = DateTime.Now,
                        DocumentStateId = 1,
                        DocumentTypeId = DocumentTypesHelper.GetToDocumentFileType(Path.GetExtension(filename)),
                        FileName = gFileName,
                        FileSize = contentLength,
                        Hash = md5,
                        Guid = guid,
                        Path = path,
                        OriginalFileName = filename,
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

        public static string GetDocumentsByUserId(string baseUrl, int userId)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<DocumentModel> models = new List<DocumentModel>();

                using (var db = new FCCPortalEntities())
                {
                    List<int> userIds = new List<int>();
                    var parent = db.Users
                        .Include(x => x.Users2.Users1)
                        .Include(x => x.Users1)
                        .FirstOrDefault(x => x.Id == userId);

                    if (parent != null && parent.Users2 != null)
                    {
                        userIds.Add(parent.Users2.Id);
                        foreach (var user in parent.Users2.Users1)
                        {
                            userIds.Add(user.Id);
                        }
                    }
                    else if(parent != null)
                    {
                        userIds.Add(parent.Id);
                        foreach (var user in parent.Users1)
                        {
                            userIds.Add(user.Id);
                        }
                    }

                    List<Documents> documents;
                    documents =  db.Documents
                        .Include(x => x.DocumentStates)
                        .Include(x => x.DocumentTypes)
                        .Include(x => x.Tasks)
                        .Include(x => x.Tasks)
                        .Where(x => userIds.Contains(x.Tasks.UserId) && x.DocumentCategoryId == 1).ToList();

                    foreach (var document in documents)
                    {
                        DocumentModel model = new DocumentModel()
                        {
                            Id = document.Id,
                            DateTime = document.Date.ToString(),
                            FileSizeBytes = document.FileSize,
                            OriginalFileName = document.OriginalFileName,
                            FileSize = Math.Round((double)(document.FileSize) / (1024 * 1024), 2),
                            StateName = document.DocumentStates.Name,
                            StateId = document.DocumentStateId,
                            TaskId = document.TaskId.ToString(),
                            TypeId = document.DocumentTypeId,
                            TypeName = document.DocumentTypes.Name,
                            Url = document.Path,
                            ServiceId = document.Tasks.ServiceId,
                            DocumentErrors = serializer.Deserialize<List<DocumentError>>(document.ErrorText ?? "")
                        };


                        List<Documents> resultDocs = db.Documents
                       .Include(x => x.DocumentStates)
                       .Include(x => x.DocumentTypes)
                       .Include(x => x.Tasks)
                       .Include(x => x.Tasks)
                       .Where(x => userIds.Contains(x.Tasks.UserId) && x.TaskId == document.TaskId && x.DocumentCategoryId == 2).ToList();
                        foreach (var rDocument in resultDocs)
                        {
                            DocumentModel rModel = new DocumentModel()
                            {
                                Id = rDocument.Id,
                                DateTime = rDocument.Date.ToString(),
                                FileSizeBytes = rDocument.FileSize,
                                OriginalFileName = rDocument.OriginalFileName,
                                FileSize = Math.Round((double)(rDocument.FileSize) / (1024 * 1024), 2),
                                StateName = rDocument.DocumentStates.Name,
                                StateId = rDocument.DocumentStateId,
                                TaskId = rDocument.TaskId.ToString(),
                                TypeId = rDocument.DocumentTypeId,
                                TypeName = rDocument.DocumentTypes.Name,
                                Url = rDocument.Path,
                                ServiceId = rDocument.Tasks.ServiceId

                            };
                            rModel.Url = baseUrl + "/" + rModel.Url;
                            model.ResultDocuments.Add(rModel);
                        }

                        //этот момент переделать, чтобы не давать полный адрес к файлу
                        model.Url = baseUrl + "/" + model.Url;

                        models.Add(model);

                    }
                }

                models = models.OrderByDescending(x => x.Id).ToList();
                return serializer.Serialize(models);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// get to all documents by user id and service id
        /// </summary>
        /// <returns></returns>
        public static string GetDocumentsByUserServiceId(string baseUrl, int userId, int serviceId)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<DocumentModel> models = new List<DocumentModel>();

                using (var db = new FCCPortalEntities())
                {
                    
                    List<Documents> documents = db.Documents
                        .Include(x => x.DocumentStates)
                        .Include(x => x.DocumentTypes)
                        .Include(x => x.Tasks)
                        .Include(x=>x.Tasks)
                        .Where(x => x.Tasks.UserId == userId && x.Tasks.ServiceId == serviceId && x.DocumentCategoryId==1).ToList();

                    foreach (var document in documents)
                    {
                        DocumentModel model = new DocumentModel()
                        {
                            Id = document.Id,
                            DateTime = document.Date.ToString(),
                            FileSizeBytes = document.FileSize,
                            OriginalFileName = document.OriginalFileName,
                            FileSize =Math.Round((double)(document.FileSize)/(1024*1024),2),
                            StateName = document.DocumentStates.Name,
                            StateId = document.DocumentStateId,
                            TaskId = document.TaskId.ToString(),
                            TypeId = document.DocumentTypeId,
                            TypeName = document.DocumentTypes.Name,
                            Url = document.Path,
                            ServiceId = serviceId,
                            DocumentErrors = serializer.Deserialize<List<DocumentError>>(document.ErrorText??"")
                        };


                        List<Documents> resultDocs = db.Documents
                       .Include(x => x.DocumentStates)
                       .Include(x => x.DocumentTypes)
                       .Include(x => x.Tasks)
                       .Include(x => x.Tasks)
                       .Where(x => x.Tasks.UserId == userId && x.TaskId==document.TaskId && x.Tasks.ServiceId == serviceId && x.DocumentCategoryId == 2).ToList();
                        foreach (var rDocument in resultDocs)
                        {
                            DocumentModel rModel = new DocumentModel()
                            {
                                Id = rDocument.Id,
                                DateTime = rDocument.Date.ToString(),
                                FileSizeBytes = rDocument.FileSize,
                                OriginalFileName = rDocument.OriginalFileName,
                                FileSize = Math.Round((double)(rDocument.FileSize) / (1024 * 1024), 2),
                                StateName = rDocument.DocumentStates.Name,
                                StateId = rDocument.DocumentStateId,
                                TaskId = rDocument.TaskId.ToString(),
                                TypeId = rDocument.DocumentTypeId,
                                TypeName = rDocument.DocumentTypes.Name,
                                Url = rDocument.Path,
                                ServiceId = serviceId

                            };
                            rModel.Url = baseUrl + "/" + rModel.Url;
                            model.ResultDocuments.Add(rModel);
                        }

                        //этот момент переделать, чтобы не давать полный адрес к файлу
                        model.Url = baseUrl +"/"+ model.Url;
                        
                        models.Add(model);

                    }
                }

                models = models.OrderByDescending(x => x.Id).ToList();
                return serializer.Serialize(models);
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// method to delete documents and their tasks
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static string DeleteDocuments(List<DocumentModel> models)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                using (FCCPortalEntities db = new FCCPortalEntities())
                {
                    foreach (var model in models)
                    {
                        DB.Documents doc = db.Documents
                            .Include(x=>x.Tasks)
                            .FirstOrDefault(x=>x.Id == model.Id);

                        if (doc != null)
                        {
                            db.Tasks.Remove(doc.Tasks);
                            db.Documents.Remove(doc);
                        }
                    }
                    db.SaveChanges();
                }
                return "Success";
            }

            catch (Exception exception)
            {
                return "Error";
            }
        }

        /// <summary>
        /// get to all documents by user id and service id
        /// </summary>
        /// <returns></returns>
        public static List<Documents> GetDocumentsByTaskId(int taskId)
        {
            try
            {
                List<DocumentModel> models = new List<DocumentModel>();

                using (var db = new FCCPortalEntities())
                {
                    List<Documents> documents = db.Documents
                        .Include(x => x.DocumentStates)
                        .Include(x => x.DocumentTypes)
                        .Include(x => x.Tasks)
                        .Include(x => x.Tasks)
                        .Where(x => x.Tasks.Id == taskId).ToList();
                    return documents;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get document by id
        /// </summary>
        /// <returns></returns>
        public static Documents GetDocumentsById(int id)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    Documents documents = db.Documents
                        .Include(x => x.DocumentStates)
                        .Include(x => x.DocumentTypes)
                        .Include(x => x.Tasks)
                        .Include(x => x.Tasks)
                        .Single(x => x.Id == id);
                    return documents;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}