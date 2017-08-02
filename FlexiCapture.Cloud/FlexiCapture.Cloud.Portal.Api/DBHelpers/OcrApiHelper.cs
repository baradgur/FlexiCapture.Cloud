using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB_OcrApi;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class OcrApiHelper
    {
        /// <summary>
        /// insert guid to user
        /// </summary>
        /// <returns></returns>
        public static string InsertGuid()
        {
            try
            {
                using (var db =new OcrApiEntities())
                {

                    ApiKey key = new ApiKey()
                    {
                        IsActive = true,
                        Key = Guid.NewGuid().ToString().ToUpper()
                    };

                    db.ApiKey.Add(key);
                    db.SaveChanges();
                    return key.Key;

                }
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}