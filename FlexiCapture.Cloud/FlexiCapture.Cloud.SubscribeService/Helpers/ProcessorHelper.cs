using FlexiCapture.Cloud.ServiceAssist.DBHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.ServiceAssist;

namespace FlexiCapture.Cloud.SubscribeService.Helpers
{
    class ProcessorHelper
    {
        internal static void MakeProcessing()
        {
            try
            {
                ServiceAssist.Assist assist = new Assist();
                assist.CheckSubscribes();
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
