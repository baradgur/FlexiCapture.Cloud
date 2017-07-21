using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.Models.Errors;
using FlexiCapture.Cloud.ServiceAssist.Models.SettingsModels;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
{
    public class EmailSettingsHelper
    {
        /// <summary>
        /// Get all user's Email settings by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static EmailSettingsViewModel GetToEmailConversionSettings(int userId)
        {
            try
            {
                EmailSettingsViewModel model = new EmailSettingsViewModel();
                model.Settings = new List<EmailSettingsModel>();

                using (var db = new FCCPortalEntities2())
                {
                    var user = db.Users
                        .Include(x => x.Users2)
                        .FirstOrDefault(x=>x.Id == userId);
                    int parentId = 0;
                    if (user != null)
                    {
                        if (user.Users2 != null)
                        {
                            parentId = user.Users2.Id;
                        }
                        else
                        {
                            parentId = user.Id;
                        }
                    }
                    var settings = (from s in db.EmailSettings
                                    where s.UserId == parentId
                                    select s);

                    var responseSettings = db.EmailResponseSettings
                        .FirstOrDefault(x => x.UserId == parentId);

                    if (responseSettings == null && parentId > 0)
                    {
                        responseSettings = new DB.EmailResponseSettings()
                        {
                            UserId = parentId,
                            CCResponse = false,
                            Addresses = "",
                            SendReply = true,
                            AddAttachment = true,
                            AddLink = true,
                            ShowJob = true
                        };
                        db.EmailResponseSettings.Add(responseSettings);
                        db.SaveChanges();
                    }
                    else if(parentId==0)
                    {
                        return null;
                    }

                    model.ResponseSettings = new EmailResponseSettingsModel()
                    {
                        Id = responseSettings.Id,
                        UserId = responseSettings.UserId,
                        ShowJob = responseSettings.ShowJob,
                        Addresses = !string.IsNullOrWhiteSpace(responseSettings.Addresses) ? responseSettings.Addresses : "",
                        CCResponse = responseSettings.CCResponse,
                        AddLink = responseSettings.AddLink,
                        SendReply = responseSettings.SendReply,
                        AddAttachment = responseSettings.AddAttachment
                    };

                    //if (settings.Count == 0)
                    //{
                    //    model.Error = new ErrorModel()
                    //    {
                    //        Name = "No settings found",
                    //        ShortDescription = "No settings found in the database.",
                    //        FullDescription = "No settings found in the database. Please add settings to use Email Attachment Conversion Service."
                    //    };
                    //    return model;
                    //}

                    foreach (var setting in settings)
                    {
                        var settingModel = new EmailSettingsModel()
                        {
                            Id = setting.Id,
                            UserId = setting.UserId,
                            AccountName = setting.AccountName,
                            Password = "",
                            Email = setting.Email,
                            Port = setting.Port,
                            Host = setting.Host,
                            UseSSL = setting.UseSSL
                        };
                        model.Settings.Add(settingModel);
                    }
                    return model;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}