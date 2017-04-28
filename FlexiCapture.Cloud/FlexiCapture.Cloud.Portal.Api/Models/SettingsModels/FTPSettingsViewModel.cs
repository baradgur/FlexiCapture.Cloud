using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;

namespace FlexiCapture.Cloud.Portal.Api.Models.SettingsModels
{
    public class FTPSettingsViewModel
    {
        /// <summary>
        /// list of settings
        /// </summary>
        public List<FTPSettingsModel> Settings { get; set; }
        /// <summary>
        /// model providing errors to user
        /// </summary>
        public ErrorModel Error { get; set; }
    }
}