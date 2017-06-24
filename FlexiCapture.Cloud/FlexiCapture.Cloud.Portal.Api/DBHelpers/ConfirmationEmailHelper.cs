using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.ServiceAssist.DB;
using UserLogins = FlexiCapture.Cloud.Portal.Api.DB.UserLogins;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    public class ConfirmationEmailHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static string ConfirmationEmail(Guid guid)
        {
            try
            {

                using (var db = new FCCPortalEntities())
                {
                    FlexiCapture.Cloud.Portal.Api.DB.UserConfirmationEmails email =
                        db.UserConfirmationEmails.FirstOrDefault(x => x.ConfirmationGuid == guid);

                    if (email == null)
                    {
                        return "Error";
                    }
                    else
                    {
                        int userLoginId = email.UserLoginId;

                        UserLogins login = db.UserLogins.FirstOrDefault(x => x.Id == userLoginId);
                        if (login != null)
                        {
                            login.UserLoginStateId = 1;
                            db.SaveChanges();
                            return "OK";
                        }
                        else
                        {
                            return "Error";
                        }
                    }


                }
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        /// <summary>
        /// add confirmation email
        /// </summary>
        public static string AddConfirmationEmail(int loginId, Guid guid)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    DateTime expirationDate = DateTime.Now;
                    expirationDate = expirationDate.AddDays(1);
                    FlexiCapture.Cloud.Portal.Api.DB.UserConfirmationEmails email = new FlexiCapture.Cloud.Portal.Api.DB.UserConfirmationEmails();
                    email.ConfirmationGuid = guid;
                    email.ExpirationDate = expirationDate;
                    email.UserLoginId = loginId;
                    db.UserConfirmationEmails.Add(email);
                    db.SaveChanges();
                    return db.UserLogins.FirstOrDefault(x => x.Id == loginId).UserName;
                }

            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}