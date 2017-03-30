using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;
using FlexiCapture.Cloud.Portal.Api.Models.Users;
using Khingal.Models.Users;


namespace FlexiCapture.Cloud.Portal.Api.Users
{
    /// <summary>
    /// модель авторизованного пользователя
    /// </summary>
    public class AuthUserModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public AuthUserModel()
        {
            try
            {
               // MapData = new List<AreaMapModel>();
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region fields
        /// <summary>
        /// данные пользователя
        /// </summary>
        public UserModel UserData { get; set; }
        /// <summary>
        /// данные авторизации
        /// </summary>
        public UserLoginModel LoginData { get; set; }

        /// <summary>
        /// service data
        /// </summary>
        public UserServiceData ServiceData { get; set; }
        /// <summary>
        /// ошибка
        /// </summary>
        public ErrorModel Error { get; set; }
//
//        /// <summary>
//        /// карта доступа
//        /// </summary>
//        public List<AreaMapModel> MapData { get; set; }
//
//        /// <summary>
//        /// набор настроек
//        /// </summary>
//        public List<SettingsModel> SettingsData { get; set; }
        #endregion
    }
}