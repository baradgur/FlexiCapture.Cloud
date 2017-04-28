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
            catch (Exception)
            {
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}