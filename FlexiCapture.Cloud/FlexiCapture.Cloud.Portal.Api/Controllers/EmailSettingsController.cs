using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;
using FlexiCapture.Cloud.Portal.Api.Models.SettingsModels;
using FlexiCapture.Cloud.Portal.Api.Helpers.ServiceSettingsHelper;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class EmailSettingsController : ApiController
    {
        // GET: api/FTPSettings
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FTPSettings/5
        public EmailSettingsViewModel Get(int userId)
        {
            try
            {
                return EmailSettingsHelper.GetToSettings(userId);
            }
            catch (Exception exception)
            {
                return new EmailSettingsViewModel()
                {
                    Error = new ErrorModel()
                    {
                        Name = "Error Email Settings",
                        ShortDescription = exception.Message,
                        FullDescription = (exception.InnerException == null) ? "" : exception.InnerException.Message.ToString()
                    }
                };
            }
        }

        // POST: api/FTPSettings
        public EmailSettingsModel Post(EmailSettingsModel model)
        {
            try
            {
                return EmailSettingsHelper.AddSettings(model);
            }
            catch (Exception exception)
            {
                return new EmailSettingsModel()
                {
                    Error = new ErrorModel()
                    {
                        Name = "Error Adding Email Settings",
                        ShortDescription = exception.Message,
                        FullDescription = (exception.InnerException == null) ? "" : exception.InnerException.Message.ToString()
                    }
                };
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
