using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.Helpers.CryptHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;
using FlexiCapture.Cloud.Portal.Api.Models.Users;
using System.Data.Entity;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;
using System.Data.Entity;
using System.Data.Entity.Core;
using FlexiCapture.Cloud.Portal.Api.Helpers.EmailHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.GeneralModels;
using FlexiCapture.Cloud.Portal.Api.Models.StoreModels;
using FlexiCapture.Cloud.Portal.Api.Users;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    public static class UsersHelper
    {
        /// <summary>
        ///    получаем данные всех пользователей
        /// </summary>
        /// <returns></returns>
        public static UsersViewModel GetToUsers(int userId, int userRoleId)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                UsersViewModel models = new UsersViewModel();


                models.UsersData = GetToUsersData(userId, userRoleId);
                models.UserRolesData = GetToUserRolesData();
                models.LoginStatesData = LoginHelper.GetToLoginStates();

                return models;
                //return serializer.Serialize(models);
            }

            catch (Exception exception)
            {
                return null;
            }
        }
        /// <summary>
        /// adding user by admin
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string AddUserAdmin(UserViewModel model)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                using (var db = new FCCPortalEntities())
                {
                    UserLogins login = db.UserLogins
                        .Include(x => x.Users)
                        .FirstOrDefault(
                            x =>
                                x.Users.Email.ToLower().Equals(model.UserData.Email.ToLower()) ||
                                x.UserName.ToLower().Equals(model.UserData.UserName.ToLower()));

                    if (login != null)
                    {
                        return serializer.Serialize(new UserViewModel()
                        {
                            Error = new ErrorModel()
                            {
                                Name = "Error registration",
                                ShortDescription = "",
                                FullDescription = "User with this e-mail address already exists"

                            }
                        });
                    }

                    DB.Users user = new DB.Users()
                    {
                        FirstName = model.UserData.FirstName,
                        LastName = model.UserData.LastName,
                        Email = model.UserData.Email,
                        PhoneNumber = model.UserData.PhoneNumber,
                        CompanyName = model.UserData.CompanyName,
                        ParentUserId = model.UserData.ParentUserId

                    };

                    db.Users.Add(user);
                    db.SaveChanges();

                    UserLogins uLogin = new UserLogins()
                    {
                        UserName = model.UserData.UserName,
                        UserPassword = PasswordHelper.Crypt.EncryptString(model.LoginData.UserPassword),
                        UserLoginStateId = model.LoginData.UserLoginStateId,
                        UserRoleId = model.UserData.UserRoleId,
                        UserId = user.Id,
                        LastLoginDate = DateTime.Now,
                        RegistrationDate = DateTime.Now
                    };
                    db.UserLogins.Add(uLogin);
                    db.SaveChanges();

                    if (user.ParentUserId.HasValue)
                    {
                        IList<DB.UserServiceSubscribes> subscribes = db.UserServiceSubscribes.Include(x => x.ServiceTypes
                                .UserProfileServiceDefault.Select(xx => xx.UserProfiles))
                            .Where(x => x.UserId == user.ParentUserId).ToList();

                        foreach (var subscribe in subscribes)
                        {
                            var profile = new NewProfileModel();
                            profile.UserId = user.Id;
                            var name = "";
                            switch (subscribe.ServiceId)
                            {
                                case 1:
                                    name = "Single File Service";
                                    break;
                                case 2:
                                    name = "Batch Service";
                                    break;
                                case 3:
                                    name = "FTP Service";
                                    break;
                                case 4:
                                    name = "Email Service";
                                    break;
                            }

                            Guid devKeyForOcrApi = new Guid();
                            if (subscribe.ServiceId == 5)
                            {
                                devKeyForOcrApi = Guid.NewGuid();
                            }

                            profile.ProfileName = name + " Default Capture Profile ";
                            var mdl = ManageUserProfileHelper.CreateNewProfile(profile);
                            //var serializer = new JavaScriptSerializer();
                            var pModel = serializer.Deserialize<ManageUserProfileModel>(mdl);

                            UserProfileServiceDefault userProfileServiceDefault = new UserProfileServiceDefault()
                            {
                                UserProfileId = pModel.Id,
                                ServiceTypeId = subscribe.ServiceId
                            };

                            db.UserProfileServiceDefault.Add(userProfileServiceDefault);

                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        if (model.ServiceData.SingleFileConversionService)
                        {
                            StoreModel stmModel = new StoreModel()
                            {
                                UserId = user.Id,
                                ServiceId = (int)Models.Enums.ServiceTypes.Single,
                                State = true
                            };
                            StoreHelper.SetStoreState(stmModel);
                        }


                        if (model.ServiceData.BatchFileConversionService)
                        {
                            StoreModel stmModel = new StoreModel()
                            {
                                UserId = user.Id,
                                ServiceId = (int)Models.Enums.ServiceTypes.Batch,
                                State = true
                            };
                            StoreHelper.SetStoreState(stmModel);
                        }

                        if (model.ServiceData.EmailAttachmentFileConversionService)
                        {
                            StoreModel stmModel = new StoreModel()
                            {
                                UserId = user.Id,
                                ServiceId = (int)Models.Enums.ServiceTypes.Email,
                                State = true
                            };
                            StoreHelper.SetStoreState(stmModel);
                        }

                        if (model.ServiceData.FTPFileConversionService)
                        {
                            StoreModel stmModel = new StoreModel()
                            {
                                UserId = user.Id,
                                ServiceId = (int)Models.Enums.ServiceTypes.FTP,
                                State = true
                            };
                            StoreHelper.SetStoreState(stmModel);
                        }
                    }

                    var response = GetToUsersData(user.Id);

                    EmailHelper.SendNewUserInfoEmail(response.UserData.FirstName, response.UserData.LastName, response.UserData.Email, response.UserData.CompanyName,
                        response.UserData.PhoneNumber, response.UserData.UserRoleName);

                    return serializer.Serialize(response);

                }



            }
            catch (Exception exception)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(new UserViewModel()
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

        /// <summary>
        ///     получаем данные пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UserModel GetToUserData(int id)
        {
            string applicationPath = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    var login = db.UserLogins
                        .Include(x => x.Users.Users2)
                        .Include(x => x.UserLoginStates)
                        .Include(x => x.UserRoleTypes)
                        .FirstOrDefault(x => x.Users.Id == id);
                    var model = new UserModel
                    {

                        LastActivityDate = login.LastLoginDate.ToString(),
                        Id = id,
                        FirstName = login.Users.FirstName,
                        LastName = login.Users.LastName,
                        UserRoleId = (int)login.UserRoleId,
                        UserRoleName = login.UserRoleTypes.Name,
                        LoginState = login.UserLoginStates.StateName,
                        UserName = login.UserName,
                        RegistrationDate = login.RegistrationDate.ToString(),
                        Email = login.Users.Email,
                        CompanyName = login.Users.CompanyName,
                        PhoneNumber = login.Users.PhoneNumber,
                        ParentUserId = login.Users.ParentUserId,
                        ParentUserName = login.Users.Users2 != null ? login.Users.Users2.FirstName + " " + login.Users.Users2.LastName : "",
                        GetUsePaymentNotif = login.Users.GetUsePaymentNotif,
                        GetReleaseUpdateNotif = login.Users.GetReleaseUpdateNotif
                    };

                    return model;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        ///     получаем данные пользователя для Communication
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<UserModel> GetToUserData(string userValue)
        {

            try
            {
                List<UserModel> models = new List<UserModel>();
                using (var db = new FCCPortalEntities())
                {
                    var logins = db.UserLogins
                        .Include(x => x.Users.Users2)
                        .Include(x => x.UserLoginStates)
                        .Include(x => x.UserRoleTypes)
                        .Where(x => x.Users.FirstName.ToLower().Contains(userValue.ToLower()) 
                        || x.Users.LastName.ToLower().Contains(userValue.ToLower())
                        || x.Users.Email.ToLower().Contains(userValue.ToLower())
                        || (x.Users.FirstName+" "+x.Users.LastName).ToLower().Contains(userValue.ToLower())
                        || (x.Users.LastName + " " + x.Users.FirstName).ToLower().Contains(userValue.ToLower())
                        );

                    foreach (var login in logins)
                    {
                        var model = new UserModel
                        {
                            LastActivityDate = login.LastLoginDate.ToString(),
                            Id = login.Users.Id,
                            FirstName = login.Users.FirstName,
                            LastName = login.Users.LastName,
                            UserRoleId = (int)login.UserRoleId,
                            UserRoleName = login.UserRoleTypes.Name,
                            LoginState = login.UserLoginStates.StateName,
                            UserName = login.UserName,
                            RegistrationDate = login.RegistrationDate.ToString(),
                            Email = login.Users.Email,
                            CompanyName = login.Users.CompanyName,
                            PhoneNumber = login.Users.PhoneNumber,
                            ParentUserId = login.Users.ParentUserId,
                            ParentUserName = login.Users.Users2 != null ? login.Users.Users2.FirstName + " " + login.Users.Users2.LastName : ""
                        };
                        models.Add(model);
                    }
                    return models;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// получение данных пользователей для UserViewModel представления
        /// </summary>
        /// <returns></returns>
        private static List<UserViewModel> GetToUsersData(int userId, int userRoleId)
        {
            try
            {
                var models = new List<UserViewModel>();
                using (FCCPortalEntities db = new FCCPortalEntities())
                {
                    List<DB.Users> users;

                    if (userRoleId == 1)
                    {
                        users = db.Users
                            .Include(x => x.UserServiceSubscribes)
                            .ToList();
                    }
                    else
                    {
                        users = db.Users
                            .Where(x => x.ParentUserId == userId)
                            .Include(x => x.UserServiceSubscribes)
                            .Include(x => x.Users2)
                            .ToList();
                    }


                    foreach (var user in users)
                    {
                        UserViewModel model = new UserViewModel();

                        model.UserData = GetToUserData(user.Id);
                        model.LoginData = LoginHelper.GetToLoginData(model.UserData.Id);
                        model.UserRoleData = GetToUserRolesData(model.UserData.UserRoleId);
                        model.ServiceData = UserServiceDataHelper.GetToServiceData(user.UserServiceSubscribes);

                        models.Add(model);
                    }
                    ;

                }
                return models;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// получение данных пользователя для UserViewModel представления
        /// </summary>
        /// <returns></returns>
        private static UserViewModel GetToUsersData(int? id)
        {
            try
            {

                using (FCCPortalEntities db = new FCCPortalEntities())
                {
                    var user = db.Users
                        .Include(x => x.UserServiceSubscribes)
                        .FirstOrDefault(x => x.Id == id);


                    UserViewModel model = new UserViewModel();

                    model.UserData = GetToUserData(user.Id);
                    model.LoginData = LoginHelper.GetToLoginData(model.UserData.Id);
                    model.UserRoleData = GetToUserRolesData(model.UserData.UserRoleId);
                    model.ServiceData = UserServiceDataHelper.GetToServiceData(user.UserServiceSubscribes);
                    return model;
                }
                return null;
            }
            catch (Exception exception)
            {
                return new UserViewModel()
                {
                    Error = new ErrorModel()
                    {
                        Name = "Error Auth",
                        ShortDescription = exception.Message,
                        FullDescription = (exception.InnerException == null) ? "" : exception.InnerException.Message.ToString()

                    }
                };
            }
        }

        /// <summary>
        /// добавление пользователя
        /// </summary>
        /// <returns></returns>
        public static string AddUser(UserProfileModel model)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                using (var db = new FCCPortalEntities())
                {
                    UserLogins login = db.UserLogins
                        .Include(x => x.Users)
                        .FirstOrDefault(
                            x =>
                                x.Users.Email.ToLower().Equals(model.Email.ToLower()) ||
                                x.UserName.ToLower().Equals(model.UserName.ToLower()));

                    if (login != null)
                    {
                        return serializer.Serialize(new UserProfileModel()
                        {
                            Error = new ErrorModel()
                            {
                                Name = "Error registration",
                                ShortDescription = "E-mail address exists",
                                FullDescription = "A user with this e-mail address already exists.  Please login or reset your password."

                            }
                        });
                    }

                    DB.Users user = new DB.Users()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,

                    };

                    db.Users.Add(user);
                    db.SaveChanges();

                    UserLogins uLogin = new UserLogins()
                    {
                        UserName = model.UserName,
                        UserPassword = PasswordHelper.Crypt.EncryptString(model.Password),
                        UserLoginStateId = 2,
                        UserRoleId = (int)Models.Enums.UserRoleTypes.AccountOwner,
                        UserId = user.Id,
                        LastLoginDate = DateTime.Now,
                        RegistrationDate = DateTime.Now
                    };
                    db.UserLogins.Add(uLogin);
                    db.SaveChanges();

                    //                    DB.Users rUser = db.Users
                    //                        .Include(x => x.UserLogins)
                    //                        .FirstOrDefault(x => x.Id == user.Id);

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
                        FullDescription = (exception.InnerException == null) ? "" : exception.InnerException.Message.ToString()

                    }
                });
            }
        }

        /// <summary>
        /// обновляем пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string UpdateUser(UserViewModel model)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                using (FCCPortalEntities db = new FCCPortalEntities())
                {
                    UserLogins login = db.UserLogins
                        .Include(x => x.Users)
                        .FirstOrDefault(
                            x =>
                            x.Users.Id != model.UserData.Id &&
                                (x.Users.Email.ToLower().Equals(model.UserData.Email.ToLower()) ||
                                x.UserName.ToLower().Equals(model.UserData.UserName.ToLower())
                                ));

                    if (login != null)
                    {
                        return serializer.Serialize(new UserViewModel()
                        {
                            Error = new ErrorModel()
                            {
                                Name = "Error registration",
                                ShortDescription = "User exists",
                                FullDescription = "User with this credentials already exists"

                            }
                        });
                    }

                    DB.Users user = db.Users.Find(Convert.ToInt32(model.UserData.Id));
                    if (user != null)
                    {
                        user.Id = model.UserData.Id;
                        user.FirstName = model.UserData.FirstName;
                        user.LastName = model.UserData.LastName;
                        user.CompanyName = model.UserData.CompanyName;
                        user.PhoneNumber = model.UserData.PhoneNumber;
                        user.Email = model.UserData.Email;
                    }
                    else
                    {
                        return serializer.Serialize(new UserViewModel()
                        {
                            Error = new ErrorModel()
                            {
                                Name = "Error Auth",
                                ShortDescription = "User not found",
                                FullDescription = "User was not found in the database!"

                            }
                        });
                    }



                    DB.UserLogins uLogin = (from s in db.UserLogins
                                            where s.UserId == model.UserData.Id
                                            select s).FirstOrDefault();

                    if (uLogin != null)
                    {
                        uLogin.UserName = model.LoginData.UserLogin;
                        uLogin.UserPassword = PasswordHelper.Crypt.EncryptString(model.LoginData.UserPassword);
                        uLogin.UserRoleId = model.UserData.UserRoleId;
                        uLogin.UserLoginStateId = model.LoginData.UserLoginStateId;
                    }
                    else
                    {
                        return serializer.Serialize(new UserViewModel()
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

                    // updating subscribes
                    StoreModel stmModel = new StoreModel()
                    {
                        UserId = user.Id,
                        ServiceId = (int)Models.Enums.ServiceTypes.Batch,
                        State = model.ServiceData.BatchFileConversionService
                    };
                    StoreHelper.SetStoreState(stmModel);

                    stmModel = new StoreModel()
                    {
                        UserId = user.Id,
                        ServiceId = (int)Models.Enums.ServiceTypes.Single,
                        State = model.ServiceData.SingleFileConversionService
                    };
                    StoreHelper.SetStoreState(stmModel);

                    stmModel = new StoreModel()
                    {
                        UserId = user.Id,
                        ServiceId = (int)Models.Enums.ServiceTypes.Email,
                        State = model.ServiceData.EmailAttachmentFileConversionService
                    };
                    StoreHelper.SetStoreState(stmModel);

                    stmModel = new StoreModel()
                    {
                        UserId = user.Id,
                        ServiceId = (int)Models.Enums.ServiceTypes.FTP,
                        State = model.ServiceData.FTPFileConversionService
                    };
                    StoreHelper.SetStoreState(stmModel);

                    var response = GetToUsersData(model.UserData.Id);

                    return serializer.Serialize(response);
                }
            }
            catch (Exception exception)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(new UserViewModel()
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



        #region helpers
        // здесь только UserRolesHelper.GetToUserRolesData

        /// <summary>
        ///     получаем все роли пользователей
        /// </summary>
        /// <returns></returns>
        private static List<UserRolesModel> GetToUserRolesData()
        {
            try
            {
                var models = new List<UserRolesModel>();

                using (var db = new FCCPortalEntities())
                {
                    var roles =
                        (from s in db.UserRoleTypes select s).ToList();

                    foreach (var role in roles)
                    {
                        models.Add(new UserRolesModel
                        {
                            Id = role.Id,
                            Name = role.Name,
                            UserRoleTypeId = role.Id
                        });
                    }
                }
                return models;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        /// <summary>
        ///     получаем все роли пользователей
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private static UserRolesModel GetToUserRolesData(int? userRoleId)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    var role =
                        (from s in db.UserRoleTypes where s.Id == userRoleId select s).FirstOrDefault();

                    var model = new UserRolesModel
                    {
                        Id = role.Id,
                        Name = role.Name,
                        ShortDescription = role.Name,
                        UserRoleTypeId = role.Id
                    };
                    return model;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// drop user password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public static string DropUserPassword(string email, string newPassword)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                string cryptPassword = PasswordHelper.Crypt.EncryptString(newPassword);

                using (var db = new FCCPortalEntities())
                {
                    DB.UserLogins login = db.UserLogins
                        .Include(x => x.Users)
                        .FirstOrDefault(x => x.Users.Email.ToLower().Equals(email.ToLower()));

                    if (login == null)
                    {
                        ErrorModel model = new ErrorModel();
                        model.Name = "Error email";
                        model.FullDescription = "Email " + email + " not found. Please check your email and try again";
                        model.ShortDescription = "Email " + email + " not exists";



                        return serializer.Serialize(model);
                    }
                    else
                    {
                        login.UserPassword = cryptPassword;
                        string userName = login.Users.FirstName + " " + login.Users.LastName;
                        EmailHelper.SendNewPasswordToEmail(userName, email, newPassword);
                        db.SaveChanges();

                        return "OK";
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorModel model = new ErrorModel();
                model.Name = "Error reset password";
                model.FullDescription = exception.Message;
                model.ShortDescription = "Error";
                return serializer.Serialize(model);
            }
        }


        public static List<int> DeleteUser(int id)
        {
            try
            {
                List<int> userIds = new List<int>();
                using (var db = new FCCPortalEntities())
                {
                    var user = db.Users
                        .Include(x => x.UserLogins.Select(xx => xx.UserConfirmationEmails))
                        .Include(x => x.UserSettings)
                        .Include(x => x.EmailSettings)
                        .Include(x => x.FTPSettings)
                        .Include(x => x.EmailResponseSettings)
                        .Include(x => x.ZipTasks.Select(xx => xx.ZipDocuments))
                        .Include(x => x.Tasks.Select(xx => xx.Documents))
                        .Include(x => x.UserServiceSubscribes)
                        .Include(x => x.UserProfiles.Select(xx => xx.UserProfileExportFormats))
                        .Include(x => x.UserProfiles.Select(xx => xx.UserProfileServiceDefault))
                        .Include(x => x.UserProfiles.Select(xx => xx.UserProfilePrintTypes))
                        .Include(x => x.UserProfiles.Select(xx => xx.UserProfileLanguages))

                        .Include(y => y.Users1.Select(x => x.UserLogins.Select(xx => xx.UserConfirmationEmails)))
                        .Include(y => y.Users1.Select(x => x.UserSettings))
                        .Include(y => y.Users1.Select(x => x.EmailSettings))
                        .Include(y => y.Users1.Select(x => x.FTPSettings))
                        .Include(y => y.Users1.Select(x => x.EmailResponseSettings))
                        .Include(y => y.Users1.Select(x => x.ZipTasks.Select(xx => xx.ZipDocuments)))
                        .Include(y => y.Users1.Select(x => x.Tasks.Select(xx => xx.Documents)))
                        .Include(y => y.Users1.Select(x => x.UserServiceSubscribes))
                        .Include(y => y.Users1.Select(x => x.UserProfiles.Select(xx => xx.UserProfileExportFormats)))
                        .Include(y => y.Users1.Select(x => x.UserProfiles.Select(xx => xx.UserProfileServiceDefault)))
                        .Include(y => y.Users1.Select(x => x.UserProfiles.Select(xx => xx.UserProfilePrintTypes)))
                        .Include(y => y.Users1.Select(x => x.UserProfiles.Select(xx => xx.UserProfileLanguages)))

                        .FirstOrDefault(x => x.Id == id);

                    if (user == null)
                    {
                        throw new ObjectNotFoundException();
                    }
                    userIds.Add(user.Id);
                    foreach (var child in user.Users1)
                    {
                        userIds.Add(child.Id);
                        foreach (var task in child.ZipTasks)
                        {
                            db.ZipDocuments.RemoveRange(task.ZipDocuments);
                        }

                        db.ZipTasks.RemoveRange(child.ZipTasks);

                        foreach (var task in child.Tasks)
                        {
                            db.Documents.RemoveRange(task.Documents);
                        }

                        db.Tasks.RemoveRange(child.Tasks);

                        foreach (var profile in child.UserProfiles)
                        {

                            db.UserProfileServiceDefault.RemoveRange(profile.UserProfileServiceDefault);
                            db.UserProfileExportFormats.RemoveRange(profile.UserProfileExportFormats);
                            db.UserProfileLanguages.RemoveRange(profile.UserProfileLanguages);
                            db.UserProfilePrintTypes.RemoveRange(profile.UserProfilePrintTypes);

                        }

                        db.UserProfiles.RemoveRange(child.UserProfiles);

                        db.FTPSettings.RemoveRange(child.FTPSettings);
                        db.EmailSettings.RemoveRange(child.EmailSettings);
                        db.EmailResponseSettings.RemoveRange(child.EmailResponseSettings);

                        foreach (var login in child.UserLogins)
                        {
                            db.UserConfirmationEmails.RemoveRange(login.UserConfirmationEmails);
                        }

                        db.UserLogins.RemoveRange(child.UserLogins);
                        db.UserSettings.RemoveRange(child.UserSettings);
                        db.UserServiceSubscribes.RemoveRange(child.UserServiceSubscribes);

                    }

                    db.Users.RemoveRange(user.Users1);

                    foreach (var task in user.ZipTasks)
                    {
                        db.ZipDocuments.RemoveRange(task.ZipDocuments);
                    }
                    db.SaveChanges();
                    db.ZipTasks.RemoveRange(user.ZipTasks);

                    foreach (var task in user.Tasks)
                    {
                        db.Documents.RemoveRange(task.Documents);
                    }

                    db.Tasks.RemoveRange(user.Tasks);

                    foreach (var profile in user.UserProfiles)
                    {

                        db.UserProfileServiceDefault.RemoveRange(profile.UserProfileServiceDefault);
                        db.UserProfileExportFormats.RemoveRange(profile.UserProfileExportFormats);
                        db.UserProfileLanguages.RemoveRange(profile.UserProfileLanguages);
                        db.UserProfilePrintTypes.RemoveRange(profile.UserProfilePrintTypes);

                    }

                    db.UserProfiles.RemoveRange(user.UserProfiles);

                    db.FTPSettings.RemoveRange(user.FTPSettings);
                    db.EmailSettings.RemoveRange(user.EmailSettings);
                    db.EmailResponseSettings.RemoveRange(user.EmailResponseSettings);

                    foreach (var login in user.UserLogins)
                    {
                        db.UserConfirmationEmails.RemoveRange(login.UserConfirmationEmails);
                    }

                    db.UserLogins.RemoveRange(user.UserLogins);
                    db.UserSettings.RemoveRange(user.UserSettings);
                    db.UserServiceSubscribes.RemoveRange(user.UserServiceSubscribes);

                    db.Users.Remove(user);

                    db.SaveChanges();
                }

                return userIds;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);

                return null;
            }
        }

        /// <summary>
        /// get to user CSV by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static HttpResponseMessage GetToUserCsv(int id)
        {
            try
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

                var user = GetToUserData(id);

                MemoryStream ms = new MemoryStream();

                string userCsv =
                    "First Name,Last Name,Role Name,E-mail,Company,Phone,Registration Date,Last Activity Date,User's Account Owner" + "\n" +
                    user.FirstName + "," + user.LastName + "," + user.UserRoleName + "," + user.Email + "," +
                    user.CompanyName + "," + user.PhoneNumber + "," + user.RegistrationDate + "," + user.LastActivityDate + "," + user.ParentUserName;



                byte[] bytes = Encoding.UTF8.GetBytes(userCsv);

                ms.Write(bytes, 0, (int)bytes.Length);
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.Content = new ByteArrayContent(bytes.ToArray());
                httpResponseMessage.Content.Headers.Add("X-File-Name", user.FirstName + " " + user.LastName + ".csv");
                httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                httpResponseMessage.Content.Headers.ContentDisposition.FileName = user.FirstName + " " + user.LastName + ".csv";
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
                return httpResponseMessage;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }

        /// <summary>
        /// get to user CSV by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="getAll"></param>
        /// <returns></returns>
        public static HttpResponseMessage GetToUserCsv(int id, bool getAll)
        {
            try
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

                string userCsv =
                   "First Name,Last Name,Role Name,E-mail,Company,Phone,Registration Date,Last Activity Date,User's Account Owner\n";

                using (var db = new FCCPortalEntities())
                {
                    var user = db.Users
                        .Include(x => x.Users1.Select(xx => xx.UserLogins.Select(xxx => xxx.UserRoleTypes)))
                        .Include(x => x.UserLogins.Select(xx => xx.UserRoleTypes))
                        .FirstOrDefault(x => x.Id == id);

                    if (user != null && user.UserLogins.Count > 0 && user.UserLogins.FirstOrDefault().UserRoleId == (int)Models.Enums.UserRoleTypes.Administrator)
                    {
                        var users = db.Users
                            .Include(x => x.UserLogins.Select(xx => xx.UserRoleTypes))
                            .Include(x => x.Users2);

                        foreach (var itemUser in users)
                        {
                            userCsv += itemUser.FirstName + "," + itemUser.LastName + "," +
                                   (itemUser.UserLogins.FirstOrDefault() != null ? itemUser.UserLogins.FirstOrDefault().UserRoleTypes.Name : "")
                                   + "," + itemUser.Email + "," +
                                   itemUser.CompanyName + "," + itemUser.PhoneNumber + "," +
                                   (itemUser.UserLogins.FirstOrDefault() != null ? itemUser.UserLogins.FirstOrDefault().RegistrationDate : null) + "," +
                                   (itemUser.UserLogins.FirstOrDefault() != null ? itemUser.UserLogins.FirstOrDefault().LastLoginDate : null)
                                   + "," + (itemUser.Users2 != null ? itemUser.Users2.FirstName + " " + itemUser.Users2.LastName : "") + "\n";
                        }
                    }
                    else if (user != null && user.UserLogins.Count > 0 && user.UserLogins.FirstOrDefault().UserRoleId == (int)Models.Enums.UserRoleTypes.AccountOwner)
                    {
                        userCsv += user.FirstName + "," + user.LastName + "," +
                                   user.UserLogins.FirstOrDefault().UserRoleTypes.Name + "," + user.Email + "," +
                                   user.CompanyName + "," + user.PhoneNumber + "," +
                                   user.UserLogins.FirstOrDefault().RegistrationDate + "," +
                                   user.UserLogins.FirstOrDefault().LastLoginDate + "," + "" + "\n";

                        foreach (var child in user.Users1)
                        {
                            userCsv += child.FirstName + "," + child.LastName + "," +
                                   (child.UserLogins.FirstOrDefault() != null ? child.UserLogins.FirstOrDefault().UserRoleTypes.Name : "")
                                   + "," + child.Email + "," +
                                   child.CompanyName + "," + child.PhoneNumber + "," +
                                   (child.UserLogins.FirstOrDefault() != null ? child.UserLogins.FirstOrDefault().RegistrationDate : null) + "," +
                                   (child.UserLogins.FirstOrDefault() != null ? child.UserLogins.FirstOrDefault().LastLoginDate : null)
                                   + "," + user.FirstName + " " + user.LastName + "\n";
                        }
                    }

                }

                MemoryStream ms = new MemoryStream();

                byte[] bytes = Encoding.UTF8.GetBytes(userCsv);

                ms.Write(bytes, 0, (int)bytes.Length);
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.Content = new ByteArrayContent(bytes.ToArray());
                httpResponseMessage.Content.Headers.Add("X-File-Name", "Users " + DateTime.Today.Date.ToShortDateString() + ".csv");
                httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                httpResponseMessage.Content.Headers.ContentDisposition.FileName = "Users " + DateTime.Today.Date.ToShortDateString() + ".csv";
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
                return httpResponseMessage;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }

        /// <summary>
        /// delete users
        /// </summary>
        /// <returns></returns>
        //public static string DeleteUsers(List<int> userIds)
        //{
        //    try
        //    {
        //        JavaScriptSerializer serializer = new JavaScriptSerializer();

        //        using (var db = new FCCPortalEntities())
        //        {
        //            var users = 
        //        }
        //        return "";
        //    }
        //    catch (Exception exception)
        //    {
        //        string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
        //        string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
        //        LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
        //                           innerException);
        //        return "";
        //    }
        //}
        public static UserModel GetToUserData(DB.Users model)
        {
            try
            {
                return new UserModel()
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CompanyName = model.CompanyName,
                    Email = model.Email
                };
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }
    }
}