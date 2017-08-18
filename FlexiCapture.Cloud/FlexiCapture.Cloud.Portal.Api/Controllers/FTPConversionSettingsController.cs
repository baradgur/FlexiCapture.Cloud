using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.EmailHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.FtpConversionSettingsHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.ServiceSettingsHelper;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;
using FlexiCapture.Cloud.Portal.Api.Models.SettingsModels;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class FTPConversionSettingsController : ApiController
    {
        public IHttpActionResult Get(int userId)
        {
            try
            {
                return Ok(FtpConversionSettingsHelper.GetSettingsByUserId(userId));
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return BadRequest("No such setting");
            }
        }

        // POST: api/FTPSettings
        public FTPConversionSettingModel Post([FromBody]FTPConversionSettingModel model)
        {
            try
            {
                return FtpConversionSettingsHelper.AddSettings(model);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        
    }
}
