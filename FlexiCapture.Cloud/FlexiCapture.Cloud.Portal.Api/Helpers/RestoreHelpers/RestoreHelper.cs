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
            catch (Exception)
            {
                return "";
            }
        }
    }
}