using System;
using System.Collections.Generic;
using System.Linq;
using FlexiCapture.Cloud.ServiceAssist.DB;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using FlexiCapture.Cloud.ServiceAssist.Models.UserProfiles;
using System.IO;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
{
    public static class TasksHelper
    {
       
        /// <summary>
        /// add task to db
        /// </summary>
        public static int AddTask(int userId, int serviceId)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
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
            catch (Exception exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// get to not executed tasks by serviceId
        /// </summary>
        /// <returns></returns>
        public static List<Tasks> GetToNotExecuteTasks(int serviceId)
        {
            try
            {
                using (var db=new FCCPortalEntities2())
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

        public static void UpdateZipTaskReponseContent(int taskId, string content)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    ZipTasks task = db.ZipTasks.FirstOrDefault(x => x.Id == taskId);
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

        public static void UpdateZipTaskState(int taskId, int stateId)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    ZipTasks task = db.ZipTasks.FirstOrDefault(x => x.Id == taskId);
                    if (task != null)
                    {
                        task.TaskStateId = stateId;
                        db.SaveChanges();

                        if (stateId == 4 || stateId == 3)
                        {
                            List<ZipTasks> cTask = db.ZipTasks.Where(x => (x.TaskStateId == 3 || x.TaskStateId == 4) && !string.IsNullOrEmpty(x.ProfileContent)).ToList();

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

        public static List<ZipTasks> GetToProcessedZipTasks()
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    return
                        db.ZipTasks.Where(x => x.TaskStateId == 2 && !string.IsNullOrEmpty(x.ResponseContent)).ToList();
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

        public static int AddZipTask(int userId, int serviceId, int outerTaskId)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    ZipTasks task = new ZipTasks()
                    {
                        CreationDate = DateTime.Now,
                        TaskStateId = 1,
                        OuterTaskId = outerTaskId,
                        UserId = userId,
                        ServiceId = serviceId
                    };
                    db.ZipTasks.Add(task);
                    db.SaveChanges();
                    return task.Id;
                }
            }
            catch (Exception exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// get to not executed tasks for zip service 
        /// </summary>
        /// <returns></returns>
        public static List<Tasks> GetToNotExecuteTasks()
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    return
                        db.Tasks.Where(x => x.TaskStateId == 1 && (x.ServiceId == 2 || x.ServiceId == 3 || x.ServiceId == 4) &&
                !string.IsNullOrEmpty(x.ProfileContent)).ToList();
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

        public static List<ZipTasks> GetToNotExecutedZipTasks()
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    return
                        db.ZipTasks.Where(x =>x.TaskStateId == 1 && !string.IsNullOrEmpty(x.ProfileContent)).ToList();
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

        public static void UpdateZipTaskProfile(int taskId, string profileContent)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    ZipTasks task = db.ZipTasks.FirstOrDefault(x => x.Id == taskId);

                    if (task != null)
                    {
                        task.ProfileContent = profileContent;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
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
                using (var db = new FCCPortalEntities2())
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
                using (var db = new FCCPortalEntities2())
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
                using (var db =new FCCPortalEntities2())
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

        public static List<string> GetToAvailableFileExtensions()
        {
            try
            {
                List<string> resultExtentions = new List<string>();
                using (var db = new FCCPortalEntities2())
                {
                    List<DocumentTypes> types = db.DocumentTypes.Select(x => x).ToList();
                    foreach (var type in types)
                    {
                        List<string> elements = type.Extension.Split(';').ToList();

                        foreach (var element in elements)
                        {
                            if (!string.IsNullOrEmpty(element))
                            {
                                resultExtentions.Add(element); 
                            }
                        }
                    }
                    return resultExtentions;
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

        public static ManageUserProfileModel CheckServiceAvailabilityByEmail(string fromAddress)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {// ServiceId == 4 means it's email attachment
                    db.Configuration.ProxyCreationEnabled = false;

                    var subscribe = db.UserServiceSubscribes
                            .Include(x => x.Users)
                            .Include(x => x.Users.UserProfiles.Select(xx => xx.UserProfileServiceDefault))
                            .Where(x => x.ServiceId == 4 && x.Users.Email == fromAddress && x.SubscribeStateId == 1)
                            .Select(x => x).FirstOrDefault();

                    int profileId = 0;
                    foreach (var userProfile in subscribe.Users.UserProfiles)
                    {
                        foreach (var defaultService in userProfile.UserProfileServiceDefault)
                        {
                            if (defaultService.ServiceTypeId == 4)
                            {
                                profileId = userProfile.Id;
                            }
                        }
                    }
                    if (profileId != 0)
                    {
                        return Helpers.ManageUserProfileHelper.GetToUserProfileById(profileId, 4);
                    }
                    return null;
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

        public static void UpdateTaskProfile(int taskId, string profileContent)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    Tasks task = db.Tasks.FirstOrDefault(x => x.Id == taskId);

                    if (task != null)
                    {
                        task.ProfileContent = profileContent;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static List<Tasks> GetToOuterTasks()
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    return
                        db.Tasks
                        .Include(x=>x.Documents)
                        .Include(x=>x.ZipTasks)
                        .Include(x=>x.ZipTasks.Select(xx=>xx.ZipDocuments))
                        .Where(x => x.TaskStateId == 2 && x.ZipTasks.Count>0).ToList();
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
    }
}
