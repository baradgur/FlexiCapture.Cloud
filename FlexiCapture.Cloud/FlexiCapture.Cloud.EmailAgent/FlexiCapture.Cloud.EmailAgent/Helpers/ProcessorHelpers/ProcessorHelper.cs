using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.EmailAgent.DB;
using FlexiCapture.Cloud.EmailAgent.DBHelpers;

namespace FlexiCapture.Cloud.EmailAgent.Helpers.ProcessorHelpers
{
    public static class ProcessorHelper
    {
        /// <summary>
        /// make processing operation
        /// </summary>
        public static void MakeProcessing()
        {
            try
            {
                //process queue
                List<Queue> notProcessingQueues = QueueHelper.GetTotProcessingQueues();
                if (notProcessingQueues.Count>0)
                LogHelper.AddLog("Found "+notProcessingQueues.Count+ " not processing queue");

                Helpers.QueueHelpers.QueueHelper.ParseQueues(notProcessingQueues);
                //check not sended emails
                Helpers.EmailHelpers.EmailHelper.ProcessNotExecutedTasks();
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
