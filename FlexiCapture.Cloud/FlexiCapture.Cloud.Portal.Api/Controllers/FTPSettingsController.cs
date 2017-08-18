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
    public class FTPSettingsController : ApiController
    {
        // GET: api/FTPSettings
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FTPSettings/5
        public FTPSettingsViewModel Get(int userId)
        {
            try
            {
                return FTPSettingsHelper.GetToSettings(userId);
            }
            catch (Exception exception)
            {
                return new FTPSettingsViewModel()
                {
                    Error = new ErrorModel()
                    {
                        Name = "Error FTP Settings",
                        ShortDescription = exception.Message,
                        FullDescription = (exception.InnerException == null) ? "" : exception.InnerException.Message.ToString()
                    }
                };
            }
        }

        // POST: api/FTPSettings
        public FTPSettingsAggregateModel Post(FTPSettingsAggregateModel model)
        {
            try
            {
                return FTPSettingsHelper.AddFtpSettingModel(model);
            }
            catch (Exception exception)
            {
                return new FTPSettingsAggregateModel()
                {
                    Error = new ErrorModel()
                    {
                        Name = "Error Adding FTP Settings",
                        ShortDescription = exception.Message,
                        FullDescription = (exception.InnerException == null) ? "" : exception.InnerException.Message.ToString()
                    }
                };
            }
        }

        // PUT: api/FTPSettings/5
        public FTPSettingsAggregateModel Put(FTPSettingsAggregateModel model)
        {
            try
            {
                return FTPSettingsHelper.UpdateFtpSettingModel(model);
            }
            catch (Exception exception)
            {
                return new FTPSettingsAggregateModel()
                {
                    Error = new ErrorModel()
                    {
                        Name = "Error Updating FTP Settings",
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
