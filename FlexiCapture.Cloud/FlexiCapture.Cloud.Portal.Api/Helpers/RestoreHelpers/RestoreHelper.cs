using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.CryptHelpers;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.RestoreHelpers
{
    public class RestoreHelper
    {
        /// <summary>
        /// restore password
        /// </summary>
        /// <returns></returns>
        public static string RestoreUserPassword(string email)
        {
            try
            {
                string newPassword = KeyGenerator.GetUniqueKey(8);
                
                return UsersHelper.DropUserPassword(email, newPassword);
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return "";
            }
        }
    }
}