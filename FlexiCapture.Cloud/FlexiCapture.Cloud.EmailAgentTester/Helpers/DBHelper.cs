using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.EmailAgent.DB;
using FlexiCapture.Cloud.EmailAgent.DBHelpers;
using FlexiCapture.Cloud.EmailAgent.Models;

namespace FlexiCapture.Cloud.EmailAgentTester.Helpers
{
    public static class DBHelper
    {
        /// <summary>
        /// add queue to database
        /// </summary>
        public static void AddQueueToDb(QueueModel model)
        {
            try
            {
                using (var db = new FCCEmailAgentEntities())
                {
                    Queue queue = new Queue()
                    {
                        EmailContent = model.EmailContent,
                        StateId = 1
                    };
                    db.Queue.Add(queue);
                    db.SaveChanges();
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
