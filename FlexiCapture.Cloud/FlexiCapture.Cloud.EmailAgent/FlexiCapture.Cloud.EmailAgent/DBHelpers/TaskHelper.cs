using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.EmailAgent.DB;

namespace FlexiCapture.Cloud.EmailAgent.DBHelpers
{
    /// <summary>
    /// helpar for task table
    /// </summary>
    public static class TaskHelper
    {
        /// <summary>
        /// update task state
        /// </summary>
        public static void UpdateTaskState(int taskId, int stateId)
        {
            try
            {
                using (var db = new FCCEmailAgentEntities())
                {
                    Tasks tasks = db.Tasks.FirstOrDefault(x => x.Id == taskId);
                    if (tasks!=null)
                    {
                        tasks.TaskStateId = stateId;
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
        /// <summary>
        /// get to all not overdue tasks
        /// </summary>
        /// <returns></returns>
        public static List<Tasks> GetToOverdueTasks()
        {
            try
            {
                using (var db = new FCCEmailAgentEntities())
                {
                    return db.Tasks.Where(x => x.TaskStateId == 1 && x.ExecutionTime < DateTime.Now).ToList();
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
        /// add task to db
        /// </summary>
        public static int AddTask(DateTime dateTime)
        {
            try
            {
                using (var db= new FCCEmailAgentEntities())
                {
                    Tasks task = new Tasks()
                    {
                        DayNumber = null,
                        ExecutionTime = dateTime,
                        Interval = null,
                        TaskTypeId = 1,
                        TaskStateId = 1

                    };
                    db.Tasks.Add(task);
                    db.SaveChanges();
                    return task.Id;
                }
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
    }
}
