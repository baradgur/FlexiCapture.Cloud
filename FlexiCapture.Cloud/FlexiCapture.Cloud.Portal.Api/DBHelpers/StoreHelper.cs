using System;
using System.Linq;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.Models.GeneralModels;
using FlexiCapture.Cloud.Portal.Api.Models.StoreModels;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    public class StoreHelper
    {
        /// <summary>
        ///     set store state
        /// </summary>
        public static void SetStoreState(StoreModel model)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    //activate 
                    if (model.State)
                    {
                        var subscribes =
                            db.UserServiceSubscribes.FirstOrDefault(
                                x => x.UserId == model.UserId && x.ServiceId == model.ServiceId);
                        if (subscribes != null)
                        {
                            subscribes.SubscribeStateId = 1;
                            db.SaveChanges();
                        }
                        else
                        {
                            var subscribe = new UserServiceSubscribes();
                            subscribe.UserId = model.UserId;
                            subscribe.ServiceId = model.ServiceId;
                            subscribe.SubscribeStateId = 1;
                            db.UserServiceSubscribes.Add(subscribe);

                            var profile = new NewProfileModel();
                            profile.UserId = model.UserId;
                            var name = "";
                            switch (model.ServiceId)
                            {
                                case 2:
                                    name = "Batch Service";
                                    break;
                                case 3:
                                    name = "FTP Service";
                                    break;
                                case 4:
                                    name = "Email Service";
                                    break;
                            }

                            profile.ProfileName = name + " Default Profile ";
                            var mdl = ManageUserProfileHelper.CreateNewProfile(profile);
                            var serializer = new JavaScriptSerializer();
                            var pModel = serializer.Deserialize<ManageUserProfileModel>(mdl);

                            db.UserProfileServiceDefault.Add(new UserProfileServiceDefault
                            {
                                UserProfileId = pModel.Id,
                                ServiceTypeId = model.ServiceId
                            });
                           
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var subscribes =
                            db.UserServiceSubscribes.FirstOrDefault(
                                x => x.UserId == model.UserId && x.ServiceId == model.ServiceId);

                        if (subscribes != null)
                        {
                            subscribes.SubscribeStateId = 2;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}