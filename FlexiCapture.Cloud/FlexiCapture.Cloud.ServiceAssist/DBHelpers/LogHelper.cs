using System;
using System.Linq;
using FlexiCapture.Cloud.ServiceAssist.DB;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
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

                    db.Log.Add(new Log() {Date = DateTime.Now, Message = message});
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
