using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;
using FlexiCapture.Cloud.Portal.Api.Models.SettingsModels;

namespace FlexiCapture.Cloud.Portal.Api.Models.SettingsModels
{
    public class FTPSettingsAggregateModel
    {
        public FTPSettingsModel InputFtpSettingsModel { get; set; }
        public FTPSettingsModel OutputFtpSettingsModel { get; set; }
        public FTPSettingsModel ExceptionFtpSettingsModel { get; set; }
        public ErrorModel Error { get; set; }
    }
}