

using Khingal.Models.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.CryptHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;
using FlexiCapture.Cloud.Portal.Api.Models.Users;


namespace Khingal.Helpers.DBHelpers.Users
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
                    var user = (from s in db.Users
                                where s.Id == id select s).ToList().LastOrDefault();

                    var login = db.UserLogins.FirstOrDefault(x => x.UserId==user.Id);
                        
                    var model = new UserModel
                    {

                        LastActivityDate = (user == null) ? "" : login.LastLoginDate.Value.ToString(),
                        Id = id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserRoleId = (int) login.UserRoleId,
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
        public static string AddUser(UserViewModel model)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
//                using (KhingalEntities db = new KhingalEntities())
//                {
//                    string cryptPassword = CryptHelpers.PasswordHelper.Crypt.EncryptString(model.LoginData.UserPassword);
//                    if (model.LoginData.IsGroupAccount) {
//                        var checkForPasswordDuplications = (from s in db.UserLogins
//                                                            where s.Pasword.Equals(cryptPassword)
//                                                            select s).FirstOrDefault();
//                        if (checkForPasswordDuplications != null) {
//                            return serializer.Serialize(new UserViewModel()
//                            {
//                                Error = new ErrorModel()
//                                {
//                                    Name = "Ошибка добавления пользователя",
//                                    ShortDescription = "Повторяющийся пароль, пользователь не был добавлен",
//                                    FullDescription = "Пользователь с групповой ролью должен иметь уникальный пароль! Измените пароль пользователя"
//                                }
//                            });
//                        }
//                    }
//                    DB.Users user = new DB.Users();
//
//                    user.LastActivityDate = null;
//                    if (!String.IsNullOrEmpty(model.UserData.BirthDayDate))
//                    {
//                        user.BirthDayDate = DateTime.Parse(model.UserData.BirthDayDate);
//                    }
//                    user.FirstName = model.UserData.FirstName;
//                    user.SecondName = model.UserData.SecondName;
//                    user.LastName = model.UserData.LastName;
//                    user.CompanyUnitId = model.UserData.CompanyUnitId;
//                    user.UserRoleId = model.UserData.UserRoleId;
//
//                    db.Users.Add(user);
//
//                    DB.UserLogins login = new DB.UserLogins()
//                    {
//                        UserLogin = model.LoginData.UserLogin,
//                        Pasword = PasswordHelper.Crypt.EncryptString(model.LoginData.UserPassword),
//                        RegistrationDate = DateTime.Now,
//                        LastLoginDate = DateTime.Now,
//                        IsGroupAccount = model.LoginData.IsGroupAccount,
//                        UserLoginStateId = model.LoginData.UserLoginStateId,
//                        UserId = user.Id
//                    };
//                    db.UserLogins.Add(login);
//                    db.SaveChanges();
//
//                    UserViewModel response = GetToUsersData(user.Id);

//                    return serializer.Serialize(response);
                return "";
           // }
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
            catch (Exception)
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
    }
}