using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.Helpers.CryptHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;
using FlexiCapture.Cloud.Portal.Api.Models.Users;
using Khingal.Models.Users;
using System.Data.Entity;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;
using System.Data.Entity;
using FlexiCapture.Cloud.Portal.Api.Helpers.EmailHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.StoreModels;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    public static class UsersHelper
    {
        /// <summary>
        ///    получаем данные всех пользователей
        /// </summary>
        /// <returns></returns>
        public static UsersViewModel GetToUsers()
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                UsersViewModel models = new UsersViewModel();
                models.UsersData = GetToUsersData();
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
                using (var db = new FCCPortalEntities2())
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
                                ShortDescription = "User exists",
                                FullDescription = "User with this credentials is exists"

                            }
                        });
                    }

                    DB.Users user = new DB.Users()
                    {
                        FirstName = model.UserData.FirstName,
                        LastName = model.UserData.LastName,
                        Email = model.UserData.Email,
                        PhoneNumber = model.UserData.PhoneNumber,
                        CompanyName = model.UserData.CompanyName

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

                    var response = GetToUsersData(user.Id);
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
                using (var db = new FCCPortalEntities2())
                {
                    var login = db.UserLogins
                        .Include(x => x.Users)
                        .Include(x => x.UserLoginStates)
                        .Include(x => x.UserRoleTypes)
                        .FirstOrDefault(x => x.Users.Id == id);
                    var model = new UserModel
                    {

                        LastActivityDate = login.LastLoginDate.ToString(),
                        Id = id,
                            FirstName = login.Users.FirstName,
                        LastName = login.Users.LastName,
                        UserRoleId = (int) login.UserRoleId,
                        UserRoleName = login.UserRoleTypes.Name,
                        LoginState = login.UserLoginStates.StateName,
                        UserName = login.UserName,
                        RegistrationDate = login.RegistrationDate.ToString(),
                        Email = login.Users.Email,
                        CompanyName = login.Users.CompanyName,
                        PhoneNumber = login.Users.PhoneNumber,

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
        /// получение данных пользователей для UserViewModel представления
        /// </summary>
        /// <returns></returns>
        private static List<UserViewModel> GetToUsersData()
        {
            try
            {
                var models = new List<UserViewModel>();
                using (FCCPortalEntities2 db = new FCCPortalEntities2())
                {
                    var users = db.Users
                        .Include(x=>x.UserServiceSubscribes)
                        .ToList();

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

                using (FCCPortalEntities2 db = new FCCPortalEntities2())
                {
                    var user = db.Users
                        .Include(x=>x.UserServiceSubscribes)
                        .FirstOrDefault(x=>x.Id == id);


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
                using (var db = new FCCPortalEntities2())
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
                                ShortDescription = "User exists",
                                FullDescription = "User with this credentials is exists"
                                
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
                        UserLoginStateId = 1,
                        UserRoleId = 3,
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
                using (FCCPortalEntities2 db = new FCCPortalEntities2())
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

                using (var db = new FCCPortalEntities2())
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
                using (var db = new FCCPortalEntities2())
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

                using (var db = new FCCPortalEntities2())
                {
                    DB.UserLogins login = db.UserLogins
                        .Include(x=>x.Users)
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
                        EmailHelper.SendNewPasswordToEmail(userName,email,newPassword);
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
    }
}