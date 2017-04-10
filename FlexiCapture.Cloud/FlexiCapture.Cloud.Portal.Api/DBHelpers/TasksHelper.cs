using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    public class TasksHelper
    {
        /// <summary>
        /// add task to db
        /// </summary>
        public static int AddTask(int userId, int serviceId)
        {
            try
            {
                using (var db =new FCCPortalEntities())
                {
                    Tasks task = new Tasks()
                    {
                        CreationDate = DateTime.Now,
                        TaskStateId = 1,
                        UserId = userId,
                        ServiceId = serviceId
                    };
                    db.Tasks.Add(task);
                    db.SaveChanges();
                    return task.Id;
                }
            }
            catch (Exception)
            {
                return -1;
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
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}