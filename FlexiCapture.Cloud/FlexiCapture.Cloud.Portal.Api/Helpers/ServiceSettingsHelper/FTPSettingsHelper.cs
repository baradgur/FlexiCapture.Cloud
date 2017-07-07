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
    public class FTPSettingsHelper
    {
        /// <summary>
        /// Get all user's FTP settings by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static FTPSettingsViewModel GetToSettings(int userId)
        {
            try
            {
                FTPSettingsViewModel model = new FTPSettingsViewModel();
                model.Settings = new List<FTPSettingsModel>();

                using (var db = new FCCPortalEntities())
                {
                    var settings = (from s in db.FTPSettings
                                    where s.UserId == userId
                                    select s).ToList();

                    //if (settings.Count == 0)
                    //{
                    //    model.Error = new ErrorModel()
                    //    {
                    //        Name = "No settings found",
                    //        ShortDescription = "No settings found in the database.",
                    //        FullDescription = "No settings found in the database. Please add settings to use FTP File Conversion Service."
                    //    };
                    //    return model;
                    //}

                    foreach (var setting in settings)
                    {
                        var settingModel = new FTPSettingsModel()
                        {
                            Id = setting.Id,
                            UserId = setting.UserId,
                            UserName = setting.UserName,
                            Password = PasswordHelper.Crypt.DecryptString(setting.Password),
                            Path = setting.Path,
                            Port = setting.Port??21,
                            Host = setting.Host,
                            UseSSL = setting.UseSSL,
                            DeleteFile = setting.DeleteFile == null ? false : (bool)setting.DeleteFile
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

        public static FTPSettingsModel UpdateFTPSettings(FTPSettingsModel model)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    var setting = (from s in db.FTPSettings
                        where s.Id == model.Id
                        select s).FirstOrDefault();

                    if (setting == null)
                    {
                        throw new ObjectNotFoundException();
                    }

                    setting.UserId = model.UserId;
                    setting.UserName = model.UserName;
                    setting.Password = PasswordHelper.Crypt.EncryptString(model.Password);
                    setting.Path = model.Path;
                    setting.Port = model.Port;
                    setting.Host = model.Host;
                    setting.UseSSL = model.UseSSL;
                    setting.DeleteFile = model.DeleteFile;

                    db.SaveChanges();

                    return GetToSetting(setting.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static FTPSettingsModel AddFTPSettings(FTPSettingsModel model)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    var dbModel = new FTPSettings()
                    {
                        UserId = model.UserId,
                        UserName = model.UserName,
                        Password = PasswordHelper.Crypt.EncryptString(model.Password),
                        Path = model.Path,
                        Port = model.Port,
                        Host = model.Host,
                        UseSSL = model.UseSSL,
                        DeleteFile = (bool)model.DeleteFile
                    };

                    db.FTPSettings.Add(dbModel);
                    db.SaveChanges();

                    return GetToSetting(dbModel.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static FTPSettingsModel GetToSetting(int id)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    var setting = (from s in db.FTPSettings
                        where s.Id == id
                        select s).FirstOrDefault();

                    if (setting == null)
                    {
                        throw new ObjectNotFoundException();
                    }
                    var model = new FTPSettingsModel()
                    {
                        Id = setting.Id,
                        UserId = setting.UserId,
                        UserName = setting.UserName,
                        Password = PasswordHelper.Crypt.DecryptString(setting.Password),
                        Path = setting.Path,
                        Port = setting.Port ?? 21,
                        Host = setting.Host,
                        UseSSL = setting.UseSSL,
                        DeleteFile = (bool)setting.DeleteFile
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