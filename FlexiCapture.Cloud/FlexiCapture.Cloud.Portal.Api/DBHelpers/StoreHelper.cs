using System;
using System.Collections.Generic;
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
                using (var db = new FCCPortalEntities())
                {
                    //activate 
                    if (model.State)
                    {
                        //var subscribes =
                        //    db.UserServiceSubscribes.FirstOrDefault(
                        //        x => x.UserId == model.UserId && x.ServiceId == model.ServiceId && x.);
                        //IList<DB.UserServiceSubscribes> subscribes = (from sub in db.UserServiceSubscribes
                        //                                              join u1 in db.Users on sub.UserId equals u1.Id
                        //                                              join u2 in db.Users on sub.UserId equals u2.ParentUserId
                        //                                              where (u1.ParentUserId == model.UserId || u2.ParentUserId == model.UserId)
                        //                                              && (sub.UserId == model.UserId || sub.UserId == u1.Id || sub.UserId
                        //                                              == u2.Id) && sub.ServiceId == model.ServiceId
                        //                                              select sub).ToList();

                        List<DB.UserServiceSubscribes> subscribess = (from sub in db.UserServiceSubscribes
                                                               //join u in db.Users on sub.UserId equals u.ParentUserId
                                                               where sub.UserId == model.UserId /*&& u.ParentUserId == model.UserId*/
                                                               && sub.ServiceId == model.ServiceId
                                                               select sub).ToList();

                        DB.UserServiceSubscribes subscribes = (from sub in db.UserServiceSubscribes
                                                                      //join u in db.Users on sub.UserId equals u.ParentUserId
                                                                      where sub.UserId == model.UserId /*&& u.ParentUserId == model.UserId*/
                                                                      && sub.ServiceId == model.ServiceId
                                                                      select sub).SingleOrDefault();

                       // DB.UserServiceSubscribes abscentSubscribe = subscribes.SingleOrDefault(x => x.ServiceId == model.ServiceId);





                        if (subscribes != null)
                        { 
                                subscribes.SubscribeStateId = 1;
                                db.SaveChanges();
                            
                        }
                        else
                        {
                            IList<DB.Users> users = (from u in db.Users
                                where
                                u.Id == model.UserId || u.ParentUserId == model.UserId
                                select u).ToList();

                            foreach (var item in users)
                            {

                                var subscribe = new UserServiceSubscribes();
                                subscribe.UserId = item.Id;
                                subscribe.ServiceId = model.ServiceId;
                                subscribe.SubscribeStateId = 1;
                                db.UserServiceSubscribes.Add(subscribe);

                                var profile = new NewProfileModel();
                                profile.UserId = item.Id;
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

                                Guid devKeyForOcrApi = new Guid();
                                if (model.ServiceId == 5)
                                {
                                    devKeyForOcrApi = Guid.NewGuid();

                                    string guid = OcrApiHelper.InsertGuid();
                                }

                                profile.ProfileName = name + " Default Capture Profile ";
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
                    }
                    else
                    {
                        DB.UserServiceSubscribes subscribes = (from sub in db.UserServiceSubscribes
                                                                      //join u in db.Users on sub.UserId equals u.ParentUserId
                                                                      where sub.UserId == model.UserId/* && u.ParentUserId == model.UserId*/
                                                                      && sub.ServiceId == model.ServiceId
                                                                      select sub).SingleOrDefault();

                        if (subscribes != null)
                        {
                            
                                subscribes.SubscribeStateId = 2;
                                db.SaveChanges();
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}