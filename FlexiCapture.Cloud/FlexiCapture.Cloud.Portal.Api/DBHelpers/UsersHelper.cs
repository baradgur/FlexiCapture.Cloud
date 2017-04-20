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
//                    var user = (from s in db.Users
//                                where s.Id == id select s).ToList().LastOrDefault();
//
//                    var login = db.UserLogins.FirstOrDefault(x => x.UserId==user.Id);

//                    var user = db.Users
//                        .Include(x => x.UserLogins.Select(y=>y))
//                        .Include(x => x.UserLogins.Select(y => y.UserLoginStates))
//                        .Include(x=>x.UserLogins.Select(y=>y.UserRoleTypes))
//                        .FirstOrDefault(x => x.Id == id);

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
                using (FCCPortalEntities db = new FCCPortalEntities())
                {
                    var users = (from s in db.Users select s).ToList();

                    foreach (var user in users)
                    {
                        UserViewModel model = new UserViewModel();

                        model.UserData = GetToUserData(user.Id);
                        model.LoginData = LoginHelper.GetToLoginData(model.UserData.Id);
                        model.UserRoleData = GetToUserRolesData(model.UserData.UserRoleId);

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
                    var user = (from s in db.Users
                        where s.Id == id
                        select s).FirstOrDefault();


                    UserViewModel model = new UserViewModel();

                    model.UserData = GetToUserData(user.Id);
                    model.LoginData = LoginHelper.GetToLoginData(model.UserData.Id);
                    model.UserRoleData = GetToUserRolesData(model.UserData.UserRoleId);
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
//                JavaScriptSerializer serializer = new JavaScriptSerializer();
//                using (KhingalEntities db = new KhingalEntities())
//                {
//                    string cryptPassword = CryptHelpers.PasswordHelper.Crypt.EncryptString(model.LoginData.UserPassword);
//                    if (model.LoginData.IsGroupAccount)
//                    {
//                        var checkForPasswordDuplications = (from s in db.UserLogins
//                                                            where s.Pasword.Equals(cryptPassword)
//                                                            select s).FirstOrDefault();
//                        if (checkForPasswordDuplications != null)
//                        {
//                            return serializer.Serialize(new UserViewModel()
//                            {
//                                Error = new ErrorModel()
//                                {
//                                    Name = "Ошибка обновления пользователя",
//                                    ShortDescription = "Повторяющийся пароль, пользователь не был обновлен",
//                                    FullDescription = "Пользователь с групповой ролью должен иметь уникальный пароль! Измените пароль пользователя"
//                                }
//                            });
//                        }
//                    }
//                    DB.Users user = db.Users.Find(Convert.ToInt32(model.UserData.Id));
//                    user.Id = model.UserData.Id;
//                    if (!String.IsNullOrEmpty(model.UserData.BirthDayDate))
//                    {
//                        user.BirthDayDate = DateTime.Parse(model.UserData.BirthDayDate);
//                    }
//                    if (!String.IsNullOrEmpty(model.UserData.LastActivityDate))
//                    {
//                        user.LastActivityDate = DateTime.Parse(model.UserData.LastActivityDate);
//                    }
//                    user.FirstName = model.UserData.FirstName;
//                    user.SecondName = model.UserData.SecondName;
//                    user.LastName = model.UserData.LastName;
//                    user.CompanyUnitId = model.UserData.CompanyUnitId;
//                    user.UserRoleId = model.UserData.UserRoleId;
//
//                    DB.UserLogins uLogin = (from s in db.UserLogins
//                        where s.UserId == model.UserData.Id
//                        select s).FirstOrDefault();
//
//                    uLogin.UserLogin = model.LoginData.UserLogin;
//                    uLogin.Pasword = PasswordHelper.Crypt.EncryptString(model.LoginData.UserPassword);
//                    uLogin.IsGroupAccount = model.LoginData.IsGroupAccount;
//                    uLogin.UserLoginStateId = model.LoginData.UserLoginStateId;
//
//                    db.SaveChanges();
//                    UserViewModel response = GetToUsersData(model.UserData.Id);
//
//                    return serializer.Serialize(response);
//                };
                return "";
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