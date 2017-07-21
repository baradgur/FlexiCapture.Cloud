using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.ServiceAssist.DB;

namespace FlexiCapture.Cloud.ServiceAssist.Interfaces
{
    /// <summary>
    /// db interface
    /// </summary>
    public interface DBInterface
    {
        /// <summary>
        /// get to all not executed tasks
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        List<Tasks> GetToNotExecutedTasks(int serviceId);
 
        /// <summary>
        /// get to processed tasks
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        List<Tasks> GetToProcessedTasks(int serviceId);

        /// <summary>
        /// update task state
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="stateId"></param>
        void UpdateTaskState(int taskId, int stateId);

        /// <summary>
        /// update response content
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="content"></param>
        void UpdateTaskReponseContent(int taskId, string content);
        /// <summary>
        /// update document state
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="stateId"></param>
        void UpdateDocumentState(int documentId, int stateId);

        /// <summary>
        /// update document states by task id
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="stateId"></param>
        void UpdateDocumentStatesByTaskId(int taskId, int stateId);

        /// <summary>
        /// get to document by task id
        /// </summary>
        /// <param name="taskId"></param>
        Documents GetToDocumentByTaskId(int taskId);
        /// <summary>
        /// get to documents by task id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        List<Documents> GetToDocumentsByTaskId(int taskId);

        /// <summary>
        /// get to settings value by param name
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        string GetSettingValueByName(string settingName);

        /// <summary>
        /// add result document to db
        /// </summary>
        int AddResultDocument(int taskId, Guid guid, string originalFileName,string realFileName,string filePath);

        /// <summary>
        /// add log
        /// </summary>
        /// <param name="message"></param>
        void AddLog(string message);

    }
}
