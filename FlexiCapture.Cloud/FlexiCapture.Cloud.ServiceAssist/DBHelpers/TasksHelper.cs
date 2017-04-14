using System;
using System.Collections.Generic;
using System.Linq;
using FlexiCapture.Cloud.ServiceAssist.DB;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
{
    public static class TasksHelper
    {
        /// <summary>
        /// get to not executed tasks
        /// </summary>
        /// <returns></returns>
        public static List<Tasks> GetToNotExecuteTasks(int serviceId)
        {
            try
            {
                using (var db=new FCCPortalEntities())
                {
                    return
                        db.Tasks.Where(x => x.TaskStateId == 1 && x.ServiceId == serviceId && !string.IsNullOrEmpty(x.ProfileContent)).ToList();
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
        /// get to not executed tasks
        /// </summary>
        /// <returns></returns>
        public static List<Tasks> GetToProcessedTasks(int serviceId)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    return
                        db.Tasks.Where(x => x.TaskStateId == 2 && x.ServiceId ==serviceId && !string.IsNullOrEmpty(x.ResponseContent)).ToList();
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
        /// update task state
        /// </summary>
        public static void UpdateTaskState(int taskId, int stateId)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    Tasks task = db.Tasks.FirstOrDefault(x => x.Id == taskId);
                    if (task != null)
                    {
                        task.TaskStateId = stateId;
                        db.SaveChanges();

                        if (stateId == 4 || stateId == 3)
                        {
                            List<Tasks> cTask = db.Tasks.Where(x => (x.TaskStateId == 3 || x.TaskStateId==4) && !string.IsNullOrEmpty(x.ProfileContent)).ToList();

                            foreach (var t in cTask)
                            {
                                t.ProfileContent = "";

                            }
                            db.SaveChanges();

                        }
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
        /// update response task response
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="content"></param>
        public static void UpdateTaskReponseContent(int taskId, string content)
        {
            try
            {
                using (var db =new FCCPortalEntities())
                {
                    Tasks task = db.Tasks.FirstOrDefault(x => x.Id == taskId);
                    if (task != null)
                    {
                        task.ResponseContent = content;
                        db.SaveChanges();
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

    }
}
