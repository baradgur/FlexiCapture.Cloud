using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.Users;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.UserServiceDataHelpers
{
    public class UserServiceDataHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static UserServiceData GetToUserDataServices(int userId)
        {
            try
            {
                List<UserServiceSubscribes> subscribes = DBHelpers.UserServiceDataHelper.GetToUserSubscribes(userId);
                UserServiceData service = new UserServiceData();
                if (subscribes != null)
                {
                    service.BatchFileConversionService = subscribes.FirstOrDefault(x => x.ServiceId == 2 && x.SubscribeStateId == 1) != null;
                    service.EmailAttachmentFileConversionService = subscribes.FirstOrDefault(x => x.ServiceId == 4 && x.SubscribeStateId == 1) != null;
                    service.FTPFileConversionService = subscribes.FirstOrDefault(x => x.ServiceId == 3 && x.SubscribeStateId == 1) != null;
                    service.OnlineWebOcrApiService = subscribes.FirstOrDefault(x=>x.ServiceId==5 && x.SubscribeStateId == 1) != null;
                }
                return service;
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
    }
}