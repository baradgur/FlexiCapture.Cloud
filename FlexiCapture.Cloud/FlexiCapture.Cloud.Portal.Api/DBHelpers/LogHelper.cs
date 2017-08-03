using System;
using System.Collections.Generic;
using System.Linq;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.Models.Users;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    /// <summary>
    /// log helper
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// add record to log
        /// </summary>
        public static void AddLog(string message)
        {
            try
            {

                using (var db = new FCCPortalEntities())
                {
                    if (db.Log.Count() > 1000)
                    {
                        db.Log.RemoveRange(db.Log.Select(x => x));
                    }

                    db.Log.Add(new Log() { Date = DateTime.UtcNow, Message = message });
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }

        public static List<Log> GetToLogs()
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    var logs = (from s in db.Log select s).ToList();

                    return logs;
                }

                //return serializer.Serialize(models);
            }

            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            };
        }
    }
}
