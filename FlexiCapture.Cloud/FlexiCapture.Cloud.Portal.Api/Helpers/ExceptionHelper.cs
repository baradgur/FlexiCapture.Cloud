using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;

namespace FlexiCapture.Cloud.Portal.Api.Helpers
{
    public static class ExceptionHelper
    {
        /// <summary>
        /// trace exception
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="exception"></param>
        public static void TraceException(string methodName, Exception exception)
        {
            try
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
            }
            catch (Exception)
            {
            }
        }
    }
}