using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.EmailAgent.DB;

namespace FlexiCapture.Cloud.EmailAgent.DBHelpers
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
                
                using (var db = new FCCEmailAgentEntities())
                {
                    if (db.Log.Count() > 1000)
                    {
                        db.Log.RemoveRange(db.Log.Select(x => x));
                    }

                    db.Log.Add(new Log() {DateTime = DateTime.Now, Message = message});
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
