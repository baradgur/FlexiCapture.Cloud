using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.EmailAgent.DB;

namespace FlexiCapture.Cloud.EmailAgent.DBHelpers
{
    public static class QueueHelper
    {
        /// <summary>
        /// get to not processing queues
        /// </summary>
        /// <returns></returns>
        public static List<Queue> GetTotProcessingQueues()
        {
            try
            {
                using (var db = new FCCEmailAgentEntities())
                {
                    return db.Queue.Where(x => x.StateId == 1).ToList();
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
        /// update state queue
        /// </summary>
        public static void UpdateStateQueue(int queueId,int stateId)
        {
            try
            {
                using (var db =new FCCEmailAgentEntities())
                {
                    Queue queue = db.Queue.FirstOrDefault(x => x.Id == queueId);
                    if (queue != null)
                    {
                        queue.StateId = stateId;
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
