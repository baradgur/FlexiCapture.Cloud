using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;
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
                }
                return service;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}