using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.Models.SettingsModels;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.EmailHelpers
{
    public class EmailResponseSettingsHelper
    {
        public static EmailResponseSettingsModel GetSettingsByUserId(int userId)
        {
            try
            {
                using (FCCPortalEntities db = new FCCPortalEntities())
                {
                    var settings = db.EmailResponseSettings
                        .SingleOrDefault(x => x.UserId == userId);

                    if (settings == null)
                        return new EmailResponseSettingsModel();

                    return new EmailResponseSettingsModel(settings.Id, settings.UserId, 
                        settings.ShowJob, settings.SendReply, 
                        settings.AddAttachment, settings.AddLink, 
                        settings.CCResponse, settings.Addresses);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void AddSettings(EmailResponseSettingsModel model)
        {
            try
            {
                using (FCCPortalEntities db = new FCCPortalEntities())
                {
                    var settings = db.EmailResponseSettings
                        .SingleOrDefault(x => x.UserId == model.UserId);

                    if (settings == null)
                        throw new NullReferenceException();


                    settings.Id = model.Id;
                    settings.UserId = model.UserId;
                    settings.AddAttachment = model.AddAttachment;
                    settings.AddLink = model.AddLink;
                    settings.Addresses = model.Addresses;
                    settings.CCResponse = model.CCResponse;
                    settings.SendReply = model.SendReply;
                    settings.ShowJob = model.ShowJob;
                    

                    db.SaveChanges();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}