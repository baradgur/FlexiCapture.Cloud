using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.Helpers.CryptHelpers;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.UserProfileHelpers
{
    public static class UserProfileHelper
    {
        private static void RecaptchaResponse(string captchaResponse)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify");
                request.ContentType = "application/json";
                request.Method = "POST";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        secret = "6LcbtB0UAAAAAMGSWHdQAI7hs7hCZOf76fFsJA-N",
                        response = captchaResponse
                    });

                    streamWriter.Write(json);
                }

                var response = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
            catch (Exception)
            {
            }
        }

        public static string UpdateUserProfile(UserProfileModel model)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                using (FCCPortalEntities db = new FCCPortalEntities())
                {

                    DB.Users user = db.Users.Find(Convert.ToInt32(model.Id));
                    if (user != null)
                    {
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.CompanyName = model.CompanyName;
                        user.PhoneNumber = model.PhoneNumber;
                        user.Email = model.Email;
                    }
                    else
                    {
                        return serializer.Serialize(new UserProfileModel()
                        {
                            Error = new ErrorModel()
                            {
                                Name = "Error Auth",
                                ShortDescription = "User not found",
                                FullDescription = "User was not found in the database!"

                            }
                        });
                    }



                    var uLogin = (from s in db.UserLogins
                                            where s.UserId == model.Id
                                            select s).FirstOrDefault();

                    if (uLogin != null)
                    {
                        uLogin.UserName = model.UserName;
                        if (model.Password != null) { 
                        uLogin.UserPassword = PasswordHelper.Crypt.EncryptString(model.Password);
                        }
                    }
                    else
                    {
                        return serializer.Serialize(new UserProfileModel()
                        {
                            Error = new ErrorModel()
                            {
                                Name = "Error Auth",
                                ShortDescription = "User's credentials not found",
                                FullDescription = "User's credentials were not found in the database!"

                            }
                        });
                    }

                    db.SaveChanges();
                    model.Id = user.Id;

                    return serializer.Serialize(model);
                }
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
                        FullDescription = exception.InnerException?.Message ?? ""

                    }
                });
            }
        }

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
                
                RecaptchaResponse(model.CaptchaResponse);
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