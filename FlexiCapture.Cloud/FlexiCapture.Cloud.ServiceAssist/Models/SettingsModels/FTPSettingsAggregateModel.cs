using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.ServiceAssist.Models.Errors;
using FlexiCapture.Cloud.ServiceAssist.Models.SettingsModels;

namespace FlexiCapture.Cloud.ServiceAssist.Models
{
    public class FTPSettingsAggregateModel
    {
        public FTPSettingsModel InputFtpSettingsModel { get; set; }
        public FTPSettingsModel OutputFtpSettingsModel { get; set; }
        public FTPSettingsModel ExceptionFtpSettingsModel { get; set; }
        public ErrorModel Error { get; set; }
    }
}