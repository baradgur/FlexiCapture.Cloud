using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    public class UserServiceDataHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<UserServiceSubscribes> GetToUserSubscribes(int userId)
        {
            try
            {
                using (var db=new FCCPortalEntities())
                {
                    List<UserServiceSubscribes> subscribes =
                        db.UserServiceSubscribes.Where(x => x.UserId == userId).ToList();
                    return subscribes;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}