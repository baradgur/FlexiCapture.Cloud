using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.Models.Users;

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
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }

        public static UserServiceData GetToServiceData(ICollection<UserServiceSubscribes> userServiceSubscribes)
        {
            try
            {
                UserServiceData response = new UserServiceData();
                foreach (var subscribe in userServiceSubscribes)
                {
                    switch (subscribe.ServiceId)
                    {
                        case (int)Models.Enums.ServiceTypes.Single:
                            response.SingleFileConversionService = subscribe.SubscribeStateId ==
                                                                   (int) Models.Enums.SubscribeStates.Subscribe;
                            break;
                        case (int)Models.Enums.ServiceTypes.Email:
                            response.EmailAttachmentFileConversionService = subscribe.SubscribeStateId ==
                                                                   (int)Models.Enums.SubscribeStates.Subscribe;
                            break;
                        case (int)Models.Enums.ServiceTypes.Batch:
                            response.BatchFileConversionService = subscribe.SubscribeStateId ==
                                                                   (int)Models.Enums.SubscribeStates.Subscribe;
                            break;
                        case (int)Models.Enums.ServiceTypes.FTP:
                            response.FTPFileConversionService = subscribe.SubscribeStateId ==
                                                                   (int)Models.Enums.SubscribeStates.Subscribe;
                            break;
                    }
                }
                return response;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                throw exception;
            }
        }
    }
}