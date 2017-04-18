using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.UserProfileHelpers
{
    public static class UserProfileHelper
    {
        /// <summary>
        /// regiter users
        /// </summary>
        /// <returns></returns>
        public static string RegistrationUser(UserProfileModel model)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                //add user to db
                model = serializer.Deserialize<UserProfileModel>(UsersHelper.AddUser(model));
                if (model.Error != null) return serializer.Serialize(model);
                //set default services
                ServicesHelper.SetDeafultServiceSubcscribeForNewUser(model.Id);
                ManageUserProfileHelper.CreateProfileForNewUser(model.Id);
                DefaultProfileHelper.GenerateDefaultProfileForService(model.Id);

                return serializer.Serialize(model);
            }
            catch (Exception exception)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(new UserProfileModel()
                {
                    Error = new ErrorModel()
                    {
                        Name = "Error Auth",
                        ShortDescription = exception.Message,
                        FullDescription = (exception.InnerException == null) ? "" : exception.InnerException.Message.ToString()

                    }
                });
            }
        }
    }
}