﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.Helpers.EmailHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.ServiceSettingsHelper;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;
using FlexiCapture.Cloud.Portal.Api.Models.SettingsModels;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class EmailResponseSettingsController : ApiController
    {
        public IHttpActionResult Get(int userId)
        {
            try
            {
                return Ok(EmailResponseSettingsHelper.GetSettingsByUserId(userId));
            }
            catch (Exception exception)
            {
                return BadRequest("No such setting");
            }
        }
        
        // POST: api/FTPSettings
        public IHttpActionResult Post([FromBody]EmailResponseSettingsModel model)
        {
            try
            {
                EmailResponseSettingsHelper.AddSettings(model);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest();
            }
        }

        // PUT: api/FTPSettings/5
        public EmailSettingsModel Put(EmailSettingsModel model)
        {
            try
            {
                return EmailSettingsHelper.UpdateSettings(model);
            }
            catch (Exception exception)
            {
                return new EmailSettingsModel()
                {
                    Error = new ErrorModel()
                    {
                        Name = "Error Updating Email Settings",
                        ShortDescription = exception.Message,
                        FullDescription = (exception.InnerException == null) ? "" : exception.InnerException.Message.ToString()
                    }
                };
            }
        }

        // DELETE: api/FTPSettings/5
        public void Delete(int id)
        {
        }
    }
}