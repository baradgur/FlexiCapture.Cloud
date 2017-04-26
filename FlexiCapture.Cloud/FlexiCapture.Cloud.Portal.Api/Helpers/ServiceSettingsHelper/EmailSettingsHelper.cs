using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;
using FlexiCapture.Cloud.Portal.Api.Models.SettingsModels;
using FlexiCapture.Cloud.Portal.Api.Helpers.CryptHelpers;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.ServiceSettingsHelper
{
    public class EmailSettingsHelper
    {
        /// <summary>
        /// Get all user's Email settings by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static EmailSettingsViewModel GetToSettings(int userId)
        {
            try
            {
                EmailSettingsViewModel model = new EmailSettingsViewModel();
                model.Settings = new List<EmailSettingsModel>();

                using (var db = new FCCPortalEntities())
                {
                    var settings = (from s in db.EmailSettings
                                    where s.UserId == userId
                                    select s).ToList();

                    if (settings.Count == 0)
                    {
                        model.Error = new ErrorModel()
                        {
                            Name = "No settings found",
                            ShortDescription = "No settings found in the database.",
                            FullDescription = "No settings found in the database. Please add settings to use Email Attachment Conversion Service."
                        };
                        return model;
                    }

                    foreach (var setting in settings)
                    {
                        var settingModel = new EmailSettingsModel()
                        {
                            Id = setting.Id,
                            UserId = setting.UserId,
                            AccountName = setting.AccountName,
                            Password = PasswordHelper.Crypt.DecryptString(setting.Password),
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
                throw ex;
            }
        }

        public static EmailSettingsModel UpdateSettings(EmailSettingsModel model)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    var setting = (from s in db.EmailSettings
                                   where s.Id == model.Id
                                   select s).FirstOrDefault();

                    if (setting == null)
                    {
                        throw new ObjectNotFoundException();
                    }

                    setting.UserId = model.UserId;
                    setting.AccountName = model.AccountName;
                    setting.Password = PasswordHelper.Crypt.EncryptString(model.Password);
                    setting.Email = model.Email;
                    setting.Port = model.Port;
                    setting.Host = model.Host;
                    setting.UseSSL = model.UseSSL;

                    db.SaveChanges();

                    return GetToSetting(setting.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static EmailSettingsModel AddSettings(EmailSettingsModel model)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    var dbModel = new EmailSettings()
                    {
                        UserId = model.UserId,
                        AccountName = model.AccountName,
                        Password = PasswordHelper.Crypt.EncryptString(model.Password),
                        Email = model.Email,
                        Port = model.Port,
                        Host = model.Host,
                        UseSSL = model.UseSSL
                    };

                    db.EmailSettings.Add(dbModel);
                    db.SaveChanges();

                    return GetToSetting(dbModel.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static EmailSettingsModel GetToSetting(int id)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    var setting = (from s in db.EmailSettings
                                   where s.Id == id
                                   select s).FirstOrDefault();

                    if (setting == null)
                    {
                        throw new ObjectNotFoundException();
                    }
                    var model = new EmailSettingsModel()
                    {
                        Id = setting.Id,
                        UserId = setting.UserId,
                        AccountName = setting.AccountName,
                        Password = PasswordHelper.Crypt.DecryptString(setting.Password),
                        Email = setting.Email,
                        Port = setting.Port,
                        Host = setting.Host,
                        UseSSL = setting.UseSSL
                    };
                    return model;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}