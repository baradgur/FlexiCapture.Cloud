using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.ServiceAssist.Models.UserProfiles
{
    public class ServiceDefaultProfileModel
    {
        /// <summary>
        /// user profile Id
        /// </summary>
        public int UserProfileId { get; set; }

        /// <summary>
        /// service type Id
        /// </summary>
        public int ServiceTypeId { get; set; }
    }
}