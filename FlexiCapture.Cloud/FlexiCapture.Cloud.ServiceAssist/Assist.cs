using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.DBHelpers;
using FlexiCapture.Cloud.ServiceAssist.Helpers;
using FlexiCapture.Cloud.ServiceAssist.Interfaces;
using FlexiCapture.Cloud.ServiceAssist.Models.SettingsModels;
using FlexiCapture.Cloud.ServiceAssist.Models.UserProfiles;

namespace FlexiCapture.Cloud.ServiceAssist
{
    public class Assist : DBInterface, FileExtensionInterface
    {
        #region  fields

        public ManageUserProfileModel UserProfile { get; set; }
        public List<Documents> Documents { get; set; }
        public List<ZipDocuments> ZipDocuments { get; set; }
        public EmailSettingsViewModel EmailSettings { get; set; }

        #endregion

        #region tasks
        /// <summary>
        /// add task to db
        /// </summary>
        public int AddTask(int userId, int serviceId)
        {
            return TasksHelper.AddTask(userId, serviceId);
        }

        public void AddErrorToDocuments(int taskId, string error)
        {
            DocumentsHelper.AddErrorToDocuments(taskId, error);
        }

        /// <summary>
        /// get to not executed tasks
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public List<Tasks> GetToNotExecutedTasks(int serviceId)
        {
            return TasksHelper.GetToNotExecuteTasks(serviceId);
        }

        public void UpdateZipDocumentStatesByZipTaskId(int taskId, int stateId)
        {
            DocumentsHelper.UpdateZipDocumentStatesByZipTaskId(taskId, stateId);
        }

        public void UpdateZipTaskState(int taskId, int stateId)
        {
            TasksHelper.UpdateZipTaskState(taskId, stateId);
        }

        public void UpdateZipTaskReponseContent(int taskId, string response)
        {
            TasksHelper.UpdateZipTaskReponseContent(taskId, response);
        }

        public List<Tasks> GetToNotExecutedTasks()
        {
            return TasksHelper.GetToNotExecuteTasks();
        }

        public List<string> GetToAvailableFileExtensions()
        {
            return TasksHelper.GetToAvailableFileExtensions();
        }

        public int AddZipTask(int userId, int serviceId, int outerTaskId)
        {
            return TasksHelper.AddZipTask(userId, serviceId, outerTaskId);
        }

        /// <summary>
        /// get to processed task
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public List<Tasks> GetToProcessedTasks(int serviceId)
        {
            return TasksHelper.GetToProcessedTasks(serviceId);
        }

        public int AddZipDocument(int taskId, FileInfo fileInfo, string originalFileName, Guid newNameGuid, string uploadName, string localName, string md5, int categoryId)
        {
            return DocumentsHelper.AddZipDocument(taskId, fileInfo, originalFileName, newNameGuid, uploadName, localName, md5, categoryId);
        }

        /// <summary>
        /// update task state
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="stateId"></param>
        public void UpdateTaskState(int taskId, int stateId)
        {
            TasksHelper.UpdateTaskState(taskId, stateId);
        }

        public List<ZipDocuments> GetZipDocumentsByZipTaskId(int taskId)
        {
            return DocumentsHelper.GetToZipDocumentsByZipTaskId(taskId);
        }

        public string ConvertProfileToRequestModel(List<ZipDocuments> zipDocuments, ManageUserProfileModel userProfile)
        {
            return ProfileToRequestModelConverter.ConvertProfileToRequestModel(zipDocuments, userProfile);
        }

        public List<ZipTasks> GetToProcessedZipTasks()
        {
            return TasksHelper.GetToProcessedZipTasks();
        }

        public List<ZipTasks> GetToNotExecutedZipTasks()
        {
            return TasksHelper.GetToNotExecutedZipTasks();
        }

        public void UpdateZipTaskProfile(int taskId, string content)
        {
            TasksHelper.UpdateZipTaskProfile(taskId, content);
        }

        public ZipDocuments GetToZipDocumentByTaskId(int taskId)
        {
            return DocumentsHelper.GetToZipDocumentByTaskId(taskId);
        }

        /// <summary>
        /// update task content
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="content"></param>
        public void UpdateTaskReponseContent(int taskId, string content)
        {
            TasksHelper.UpdateTaskReponseContent(taskId, content);
        }
        #endregion

        #region documents
        /// <summary>
        /// update document state
        /// </summary>
        public void UpdateDocumentState(int documentId, int stateId)
        {
            DocumentsHelper.UpdateDocumentState(documentId, stateId);
        }

        /// <summary>
        /// update document state by task ID
        /// </summary>
        public void UpdateDocumentStatesByTaskId(int taskId, int stateId)
        {
            DocumentsHelper.UpdateDocumentStatesByTaskId(taskId, stateId);
        }

        /// <summary>
        /// get to document by task id
        /// </summary>
        /// <param name="taskId"></param>
        public Documents GetToDocumentByTaskId(int taskId)
        {
            return DocumentsHelper.GetToDocumentByTaskId(taskId);
        }

        /// <summary>
        /// get to documents by task id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public List<Documents> GetToDocumentsByTaskId(int taskId)
        {
            return DocumentsHelper.GetToDocumentsByTaskId(taskId);
        }


        /// <summary>
        /// get to settings value by param name
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public string GetSettingValueByName(string settingName)
        {
            return SettingsHelper.GetSettingValueByName(settingName);
        }

        public void AddErrorToZipDocuments(int taskId, string errorText)
        {
            DocumentsHelper.AddErrorToZipDocuments(taskId, errorText);
        }

        /// <summary>
        /// add result document to db
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="guid"></param>
        /// <param name="originalFileName"></param>
        public int AddResultDocument(int taskId, Guid guid, string originalFileName, string realFileName, string filePath)
        {
            return DocumentsHelper.AddResultDocument(taskId, guid, originalFileName, realFileName, filePath);
        }


        /// <summary>
        /// add log
        /// </summary>
        /// <param name="message"></param>
        public void AddLog(string message)
        {
            LogHelper.AddLog(message);
        }


        #endregion

        /// <summary>
        /// get to file extension by type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetToFileExtension(string type)
        {
            return ExportFormatHelper.GetFileExtensionByExportType(type);
        }

        public ManageUserProfileModel CheckServiceAvailabilityByEmail(string fromAddress)
        {
            return TasksHelper.CheckServiceAvailabilityByEmail(fromAddress);
        }

        public string GetMD5HashFromFile(string uploadPath)
        {
            return MD5Helper.GetMD5HashFromFile(uploadPath);
        }

        public int AddDocument(int taskId, FileInfo attachment, string originalFileName, Guid newNameGuid, string uploadName, string localName, string md5, int categoryId, bool showJob)
        {
            return DocumentsHelper.AddDocument(taskId, attachment, originalFileName, newNameGuid, uploadName, localName, md5, categoryId, showJob);
        }

        public void UpdateTaskProfile(int taskId, string content)
        {
            TasksHelper.UpdateTaskProfile(taskId, content);
        }

        public List<Documents> GetDocumentsByTaskId(int taskId)
        {
            return DocumentsHelper.GetToDocumentsByTaskId(taskId);
        }

        public string ConvertProfileToRequestModel(List<Documents> documents, ManageUserProfileModel userProfile)
        {
            return ProfileToRequestModelConverter.ConvertProfileToRequestModel(documents, userProfile);
        }

        public ManageUserProfileModel GetUserProfile(int objUserId, int i)
        {
            return Helpers.ManageUserProfileHelper.GetToUserProfile(objUserId, i);
        }

        public void AddResultZipDocument(int taskId, Guid guid, string originalFileName, string realFileName, string filePath)
        {
            DocumentsHelper.AddResultZipDocument(taskId, guid, originalFileName, realFileName, filePath);
        }

        public List<Tasks> GetToOuterTasks()
        {
            return TasksHelper.GetToOuterTasks();
        }

        public void UpdateDocumentErrorsFromZipDocs(int outerTaskId)
        {
            DocumentsHelper.UpdateDocumentErrorsFromZipDocs(outerTaskId);
        }

        public void SendEmailResponseFail(string email, string text, string ccAddresses)
        {
            EmailHelper.SendEmailResponseFail(email, text, ccAddresses);
        }
        public void SendEmailResponseFail(int userId, string text, string ccAddresses)
        {
            EmailHelper.SendEmailResponseFail(userId, text, ccAddresses);
        }
        public EmailSettingsViewModel GetToEmailConversionSettings(int userId)
        {
            return EmailSettingsHelper.GetToEmailConversionSettings(userId);
        }

        public void SendEmailResponse(int taskUserId, List<Tuple<int, string>> downloadIds, List<Tuple<string, string>> attachmentsLinks, string ccAddresses, string text)
        {
            EmailHelper.SendEmailResponse(taskUserId, downloadIds, attachmentsLinks, ccAddresses, text);
        }
    }
}
