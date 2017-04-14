using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.DBHelpers;
using FlexiCapture.Cloud.ServiceAssist.Interfaces;

namespace FlexiCapture.Cloud.ServiceAssist
{
    public class Assist:DBInterface,FileExtensionInterface
    {
        #region tasks
        /// <summary>
        /// get to not executed tasks
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public List<Tasks> GetToNotExecutedTasks(int serviceId)
        {
            return TasksHelper.GetToNotExecuteTasks(serviceId);
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

        /// <summary>
        /// update task state
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="stateId"></param>
        public void UpdateTaskState(int taskId, int stateId)
        {
            TasksHelper.UpdateTaskState(taskId,stateId);
        }

        /// <summary>
        /// update task content
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="content"></param>
        public void UpdateTaskReponseContent(int taskId, string content)
        {
            TasksHelper.UpdateTaskReponseContent(taskId,content);
        }

        #endregion

        #region documents
        /// <summary>
        /// update document state
        /// </summary>
        public void UpdateDocumentState(int documentId, int stateId)
        {
            DocumentsHelper.UpdateDocumentState(documentId,stateId);
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

        /// <summary>
        /// add result document to db
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="guid"></param>
        /// <param name="originalFileName"></param>
        public void AddResultDocument(int taskId, Guid guid, string originalFileName,string realFileName,string filePath)
        {
            DocumentsHelper.AddResultDocument(taskId,guid,originalFileName,realFileName,filePath);
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
    }
}
