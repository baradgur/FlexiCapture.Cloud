using System;
using System.Linq;
using FlexiCapture.Cloud.Portal.Api.DB;

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
                
                using (var db = new FCCPortalEntities2())
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
