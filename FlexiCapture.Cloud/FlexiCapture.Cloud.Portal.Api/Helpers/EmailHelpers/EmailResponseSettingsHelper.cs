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

        public static EmailResponseSettingsModel AddSettings(EmailResponseSettingsModel model)
        {
            try
            {
                using (FCCPortalEntities db = new FCCPortalEntities())
                {
                    var settings = db.EmailResponseSettings
                        .FirstOrDefault(x => x.Id == model.Id);

                    if (settings != null)
                    {
                        settings.UserId = model.UserId;
                        settings.AddAttachment = model.AddAttachment;
                        settings.AddLink = model.AddLink;
                        settings.Addresses = model.Addresses;
                        settings.CCResponse = model.CCResponse;
                        settings.SendReply = model.SendReply;
                        settings.ShowJob = model.ShowJob;

                        db.SaveChanges();
                    }
                    else
                    {
                        settings = new DB.EmailResponseSettings();
                        settings.UserId = model.UserId;
                        settings.AddAttachment = model.AddAttachment;
                        settings.AddLink = model.AddLink;
                        settings.Addresses = model.Addresses;
                        settings.CCResponse = model.CCResponse;
                        settings.SendReply = model.SendReply;
                        settings.ShowJob = model.ShowJob;

                        db.EmailResponseSettings.Add(settings);
                        db.SaveChanges();
                        model.Id = settings.Id;
                    }

                    return model;

                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}