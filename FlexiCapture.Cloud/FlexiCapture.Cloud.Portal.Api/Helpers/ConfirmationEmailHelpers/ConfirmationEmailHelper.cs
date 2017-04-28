using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                //string url = "http://portal.datacapture.cloud/#/confirmEmail?guid=" + guid;
                string url = "http://localhost/FCCPortal#/confirmEmail?guid=" + guid;
                if (!string.IsNullOrEmpty(email))
                EmailHelpers.EmailHelper.SendConfirmationEmail(email,url);
            }
            catch (Exception)
            {
            }
        }
    }
}