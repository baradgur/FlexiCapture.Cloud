using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;

namespace FlexiCapture.Cloud.Portal.Api.Models.UserProfiles
{
    /// <summary>
    /// user profile
    /// </summary>
    public class UserProfileModel
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// error
        /// </summary>
        public ErrorModel Error { get; set; }

        /// <summary>
        /// captcha response
        /// </summary>
        public string CaptchaResponse { get; set; }
    }
}