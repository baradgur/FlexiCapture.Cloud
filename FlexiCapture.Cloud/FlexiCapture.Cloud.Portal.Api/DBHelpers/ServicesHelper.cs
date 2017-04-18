using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    public static  class ServicesHelper
    {
        /// <summary>
        /// set subscribe
        /// </summary>
        /// <param name="userId"></param>
        public static void SetDeafultServiceSubcscribeForNewUser(int userId)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    db.UserServiceSubscribes.Add(new UserServiceSubscribes()
                    {
                        UserId = userId,
                        ServiceId = 1,
                        SubscribeStateId = 1
                    });
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}