using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;

using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.DBHelpers;
using FlexiCapture.Cloud.ServiceAssist.Helpers;
using FlexiCapture.Cloud.ServiceAssist.Models;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
{
    public enum FtpSettingsTypes
    {
        InputType = 1,
        OutputType = 2,
        ExceptionsType = 3
    }

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
                model.Settings = new List<FTPSettingsAggregateModel>();

                using (var db = new FCCPortalEntities2())
                {
                    // возьмем из базы все-все настройки для дальнейшего 
                    // разбиения на тройки
                    var settingsAll = (from s in db.FTPSettings
                                       where s.UserId == userId || (s.UserId == userId &&
                                       s.ParentId == userId)
                                       select s).ToList();

                    // вычленим основные настройки (input settings)
                    var inputSettings = (from s in settingsAll
                                         where s.ParentId == null
                                         select s).ToList();

                    // вычленим тройку настроек
                    foreach (var settingItem in inputSettings)
                    {
                        FTPSettingsAggregateModel aggregateModel = new FTPSettingsAggregateModel();

                        var settingsTriple = (from s in settingsAll
                                              where s.Id == settingItem.Id ||
                                                    s.ParentId == settingItem.Id
                                              select s).ToList();

                        foreach (var settingTripleItem in settingsTriple)
                        {
                            FTPSettingsModel settingsModel = new FTPSettingsModel()
                            {
                                Id = settingTripleItem.Id,
                                DeleteFile = settingTripleItem.DeleteFile ?? false,
                                Host = settingTripleItem.Host,
                                Password = PasswordHelper.Crypt.DecryptString(settingTripleItem.Password),
                                Path = settingTripleItem.Path,
                                Port = settingTripleItem.Port ?? 0,
                                UserId = settingTripleItem.UserId,
                                UserName = settingTripleItem.UserName,
                                UseSSL = settingTripleItem.UseSSL,
                                Enabled = settingTripleItem.Enabled
                            };

                            switch (settingTripleItem.FtpServiceType)
                            {
                                case 1:
                                    aggregateModel.InputFtpSettingsModel = settingsModel;
                                    break;
                                case 2:
                                    aggregateModel.OutputFtpSettingsModel = settingsModel;
                                    break;
                                case 3:
                                    aggregateModel.ExceptionFtpSettingsModel = settingsModel;
                                    break;

                            }



                        }

                        model.Settings.Add(aggregateModel);
                    }
                    return model;
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                throw exception;
            }
        }

        public static FTPSettingsAggregateModel UpdateFtpSettingModel(FTPSettingsAggregateModel model)
        {
            model.InputFtpSettingsModel = UpdateFTPSettings(model.InputFtpSettingsModel,
                FtpSettingsTypes.InputType);
            model.OutputFtpSettingsModel = UpdateFTPSettings(model.OutputFtpSettingsModel,
                FtpSettingsTypes.OutputType);
            model.ExceptionFtpSettingsModel = UpdateFTPSettings(model.ExceptionFtpSettingsModel,
                FtpSettingsTypes.ExceptionsType);

            return model;

        }

        public static FTPSettingsModel UpdateFTPSettings(FTPSettingsModel model, FtpSettingsTypes type)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
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
                    setting.Enabled = model.Enabled;

                    db.SaveChanges();

                    return GetToSetting(setting.Id);
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                throw exception;
            }
        }

        public static FTPSettingsAggregateModel AddFtpSettingModel(FTPSettingsAggregateModel model)
        {
            model.InputFtpSettingsModel = AddFTPSettings(model.InputFtpSettingsModel,
                FtpSettingsTypes.InputType);
            model.OutputFtpSettingsModel = AddFTPSettings(model.OutputFtpSettingsModel,
                FtpSettingsTypes.OutputType);
            model.ExceptionFtpSettingsModel = AddFTPSettings(model.ExceptionFtpSettingsModel,
                FtpSettingsTypes.ExceptionsType);

            return model;

        }

        public static FTPSettingsModel AddFTPSettings(FTPSettingsModel model, FtpSettingsTypes type)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
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
                        Enabled = model.Enabled,
                        DeleteFile = (bool)model.DeleteFile
                    };

                    db.FTPSettings.Add(dbModel);
                    db.SaveChanges();

                    return GetToSetting(dbModel.Id);
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                throw exception;
            }
        }

        public static FTPSettingsModel GetToSetting(int id)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
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
                        DeleteFile = (bool)setting.DeleteFile,
                        Enabled = setting.Enabled
                    };
                    return model;
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                throw exception;
            }

        }

        public static FTPSettingsModel GetToSettingByUserId(int userId)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    var setting = (from s in db.FTPSettings
                                   where s.UserId == userId
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
                        DeleteFile = (bool)setting.DeleteFile,
                        Enabled = setting.Enabled
                    };
                    return model;
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                throw exception;
            }

        }

        /// <summary>
        /// Через этот ни разу не костыльный метод проверяем 
        /// наличие Аутпут/Эксепшон настроек 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool CheckOutputExceptionSettings(int parentId)
        {
            using (var db = new FCCPortalEntities2())
            {
                var q = (from st in db.FTPSettings
                         where st.ParentId == parentId
                         select st).ToList().Count();

                return q == 2;
            }
        }
    }
}