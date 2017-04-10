using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.ManageUserHelpers
{
    public static class ManageUserHelper
    {
        /// <summary>
        /// get to new profile
        /// </summary>
        /// <returns></returns>
        private static string GetToNewUserProfile()
        {
            try
            {
                ManageUserProfileModel model = new ManageUserProfileModel();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                
                return serializer.Serialize(model);
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// get to user profile by Id
        /// </summary>
        /// <returns></returns>
        public static string GetToProfileById(int profileId)
        {
            try
            {
                ManageUserProfileModel model = ManageUserProfileHelper.GetToUserProfileById(profileId);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(model);
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// get to manage user profile
        /// </summary>
        /// <returns></returns>
        public static string GetToManageUserProfile(int profileId)
        {
            try
            {
                switch (profileId)
                {
                    case -1:
                        return GetToNewUserProfile();
                        return "";

                    default:
                        return GetToProfileById(profileId);
                        return "";
                }
                
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}