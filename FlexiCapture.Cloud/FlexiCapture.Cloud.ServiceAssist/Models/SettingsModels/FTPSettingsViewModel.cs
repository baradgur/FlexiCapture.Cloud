using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.ServiceAssist.Models.Errors;

namespace FlexiCapture.Cloud.ServiceAssist.Models
{
    public class FTPSettingsViewModel
    {
        /// <summary>
        /// list of settings
        /// </summary>
        public List<FTPSettingsAggregateModel> Settings { get; set; }
        /// <summary>
        /// model providing errors to user
        /// </summary>
        public ErrorModel Error { get; set; }
    }
}