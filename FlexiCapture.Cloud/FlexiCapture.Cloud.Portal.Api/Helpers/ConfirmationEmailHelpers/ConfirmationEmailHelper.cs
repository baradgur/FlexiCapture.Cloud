using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.ConfirmationEmailHelpers
{
    public static class ConfirmationEmailHelper
    {
        /// <summary>
        /// send confirmation user email
        /// </summary>
        public static void SendConfirmUserEmail(int userId)
        {
            try
            {
                int loginId = LoginHelpers.LoginHelper.GetToLoginIdByUserId(userId);
                Guid guid = Guid.NewGuid();

                string email =DBHelpers.ConfirmationEmailHelper.AddConfirmationEmail(loginId,guid);
                string url = "http://portal.datacapture.cloud/#/confirmEmail?guid=" + guid;
                //string url = "http://localhost/FCCPortal#/confirmEmail?guid=" + guid;
                if (!string.IsNullOrEmpty(email))
                EmailHelpers.EmailHelper.SendConfirmationEmail(email,url);
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