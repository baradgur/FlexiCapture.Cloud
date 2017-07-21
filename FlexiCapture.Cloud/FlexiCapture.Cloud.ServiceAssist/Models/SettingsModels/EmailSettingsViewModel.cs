using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.ServiceAssist.Models.Errors;

namespace FlexiCapture.Cloud.ServiceAssist.Models.SettingsModels
{
    public class EmailSettingsViewModel
    {
        /// <summary>
        /// list of settings
        /// </summary>
        public List<EmailSettingsModel> Settings { get; set; }
        /// <summary>
        /// respond settings
        /// </summary>
        public EmailResponseSettingsModel ResponseSettings { get; set; }
        /// <summary>
        /// model providing errors to user
        /// </summary>
        public ErrorModel Error { get; set; }
    }
}