using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.SettingsModels
{
    public class FTPConnectionCheckResultModel
    {
        public bool InputCheckResult { get; set; }
        public bool OutputCheckResult { get; set; }
        public bool ExceptionCheckResult { get; set; }
    }
}