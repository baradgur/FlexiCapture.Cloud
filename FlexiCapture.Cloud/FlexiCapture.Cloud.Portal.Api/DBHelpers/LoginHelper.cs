using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.Helpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.CryptHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.Users;
using Khingal.Models.Users;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    /// <summary>
    ///     login db helper
    /// </summary>
    public static class LoginHelper
    {
        /// <summary>
        ///     login
        /// </summary>
        /// <returns></returns>
        public static UserLogins LoginUser(UserLoginModel model)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    var cryptPass = PasswordHelper.Crypt.EncryptString(model.UserPassword);

                    var userLogin =
                        db.UserLogins.Where(x => x.UserName.Equals(model.UserLogin) && x.UserPassword.Equals(cryptPass))
                            .Include(x => x.Users)
                            .Include(x => x.UserRoleTypes).Select(x => x)
                            .Include(x => x.UserLoginStates).Select(x => x)
                            .Select(x => x).FirstOrDefault();
                    if (userLogin != null)
                    {
                        userLogin.LastLoginDate = DateTime.Now;
                        db.SaveChanges();
                    }

                    return userLogin;
                }
            }
            catch (Exception exception)
            {
                ExceptionHelper.TraceException(MethodBase.GetCurrentMethod().Name, exception);
                return null;
            }
        }

        /// <summary>
        /// получение данных пользователя из таблицы UserLogins и UserLoginStates
        /// </summary>
        public static UserLoginModel GetToLoginData(int? userId)
        {
            try
            {
                if (userId == null)
                    return null;
                using (var db = new FCCPortalEntities())
                {
//                   
                    UserLogins login =
                        db.UserLogins
                            .Include(x => x.Users)
                            .Include(x => x.UserRoleTypes).Select(x => x)
                            .Include(x => x.UserLoginStates).Select(x => x)
                            .Where(x=>x.Users.Id==userId)
                            .Select(x => x).FirstOrDefault();

                    //string password = KeyGenerator.GetUniqueKey(8);
                    
                    if (login != null)
                    {
                        var model = new UserLoginModel
                        {
                            Id = login.Id,
                            UserId = login.Users.Id,
                            UserPassword = PasswordHelper.Crypt.DecryptString(login.UserPassword),
                            LastLoginDate = login.LastLoginDate,
                            UserLoginStateId = login.UserLoginStateId,
                            UserLoginStateName = login.UserLoginStates.StateName,
                        };
                        //if (login.s.RegistrationDate != null) { model.RegistrationDate = login.s.RegistrationDate.Value; }
                        return model;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// получение данных возможны состояний учетной записи пользователя
        /// </summary>
        public static List<UserLoginStatesModel> GetToLoginStates()
        {
            try
            {
                List<UserLoginStatesModel> models = new List<UserLoginStatesModel>();
                using (var db = new FCCPortalEntities())
                {
                    var states =
                        (from s in db.UserLoginStates
                         select s).ToList();

                    if (states != null)
                    {
                        foreach (var state in states)
                        {
                            var model = new UserLoginStatesModel
                            {
                                Id = state.Id,
                                Name = state.StateName,
                               
                            };
                            models.Add(model);
                        }
                        return models;
                    }

                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}