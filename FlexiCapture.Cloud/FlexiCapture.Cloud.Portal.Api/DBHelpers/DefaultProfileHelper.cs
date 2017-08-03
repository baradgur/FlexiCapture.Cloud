using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    public class DefaultProfileHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static void SetNewDefaultProfile(int profileId, int newProfileId, int serviceId)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    UserProfileServiceDefault defaults =
                        db.UserProfileServiceDefault.FirstOrDefault(x => x.ServiceTypeId == serviceId && x.UserProfileId == profileId);
                    if (defaults==null) return;
                    
                    db.UserProfileServiceDefault.Remove(defaults);
                    db.SaveChanges();
                    db.UserProfileServiceDefault.Add(new UserProfileServiceDefault()
                    {
                        UserProfileId = newProfileId,
                        ServiceTypeId = serviceId
                    });
                    db.SaveChanges();

                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);

            }
        }

        public static void GenerateDefaultProfileForService(int userId, int serviceId=1)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    UserProfileServiceDefault defaults = new UserProfileServiceDefault();

                    var profile = db.UserProfiles.FirstOrDefault(x => x.UserId == userId);
 
                    if (profile==null) return;
                    int userProfileId = profile.Id;
                    db.SaveChanges();
                    db.UserProfileServiceDefault.Add(new UserProfileServiceDefault()
                    {
                        UserProfileId = userProfileId,
                        ServiceTypeId = serviceId
                    });
                    db.SaveChanges();

                }
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