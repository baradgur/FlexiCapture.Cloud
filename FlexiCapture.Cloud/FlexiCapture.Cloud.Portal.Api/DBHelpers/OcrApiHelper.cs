using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.DB_OcrApi;
using FlexiCapture.Cloud.Portal.Api.Models.OcrApiKeyModels;
using Microsoft.Ajax.Utilities;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class OcrApiHelper
    {
        /// <summary>
        /// insert guid to user in ocr.api database
        /// </summary>
        /// <returns></returns>
        public static string InsertGuid(string guid)
        {
            try
            {
                using (var db = new OcrApiEntities())
                {

                    ApiKey key = new ApiKey()
                    {
                        IsActive = true,
                        Key = guid
                    };

                    db.ApiKey.Add(key);
                    db.SaveChanges();
                    return key.Key;

                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return "";
            }
        }
        /// <summary>
        /// update guid in ocr.api database
        /// </summary>
        /// <param name="keyStr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UpdateGuid(string keyStr, bool value)
        {
            try
            {
                using (var db = new OcrApiEntities())
                {

                    var key = db.ApiKey.FirstOrDefault(x => x.Key == keyStr);

                    if (key != null)
                    {
                        key.IsActive = value;
                        db.SaveChanges();
                        return key.Key;
                    }
                    else
                    {
                        return "";
                    }


                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return "";
            }
        }

        public static List<OcrApiKeyModel> GetOcrApiKeys(int id)
        {
            try
            {
                List<OcrApiKeyModel> models = new List<OcrApiKeyModel>();
                using (var db = new FCCPortalEntities())
                {
                    var dbKeys = db.OcrApiKeys.Where(x => x.UserId == id);

                    foreach (var dbKey in dbKeys)
                    {
                        OcrApiKeyModel model = new OcrApiKeyModel()
                        {
                            Id = dbKey.Id,
                            UserId = dbKey.UserId,
                            Key = dbKey.Key,
                            IsActive = dbKey.IsActive,
                        };
                        models.Add(model);
                    }
                }
                return models;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }

        public static OcrApiKeyModel AddOcrApiKey(OcrApiKeyModel model)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    OcrApiKeys dbKey = new OcrApiKeys()
                    {
                        UserId = model.UserId,
                        Key = Guid.NewGuid().ToString().ToUpper(),
                        IsActive = true
                    };

                    string guid = InsertGuid(dbKey.Key);

                    if (!guid.IsNullOrWhiteSpace())
                    {
                        db.OcrApiKeys.Add(dbKey);
                        db.SaveChanges();

                        model.Id = dbKey.Id;
                        model.Key = dbKey.Key;
                        model.IsActive = dbKey.IsActive;
                        return model;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }

        public static OcrApiKeyModel UpdateOcrApiKey(OcrApiKeyModel model)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    OcrApiKeys dbKey = db.OcrApiKeys.FirstOrDefault(x => x.Id == model.Id);

                    if (dbKey != null)
                    {
                        dbKey.UserId = model.UserId;
                        dbKey.IsActive = model.IsActive;

                        string guid = UpdateGuid(dbKey.Key, model.IsActive);

                        if (!guid.IsNullOrWhiteSpace())
                        {
                            db.SaveChanges();

                            model.Id = dbKey.Id;
                            model.Key = dbKey.Key;
                            model.IsActive = dbKey.IsActive;
                            return model;
                        }
                    }
                    return null;
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }

        public static int DeleteKey(int id)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    OcrApiKeys dbKey = db.OcrApiKeys.FirstOrDefault(x => x.Id == id);

                    if (dbKey != null)
                    {
                        string guid = DeleteGuid(dbKey.Key);

                        if (!guid.IsNullOrWhiteSpace())
                        {
                            db.OcrApiKeys.Remove(dbKey);
                            db.SaveChanges();
                            return id;
                        }
                    }
                    return 0;
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return 0;
            }
        }

        /// <summary>
        /// delete guid from ocrApi database
        /// </summary>
        /// <param name="dbKeyKey"></param>
        /// <returns></returns>
        private static string DeleteGuid(string keyStr)
        {
            try
            {
                using (var db = new OcrApiEntities())
                {

                    var key = db.ApiKey.FirstOrDefault(x => x.Key == keyStr);

                    if (key != null)
                    {
                        db.ApiKey.Remove(key);
                        db.SaveChanges();
                    }

                    return keyStr;
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return "";
            }
        }
    }
}