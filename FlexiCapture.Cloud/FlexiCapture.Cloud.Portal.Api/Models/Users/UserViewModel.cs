using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;
using FlexiCapture.Cloud.Portal.Api.Models.Users;

namespace Khingal.Models.Users
{       /// <summary>
        /// модель пользователя определяет представление
        /// пользователя в таблице пользователей
        /// </summary>
    public class UserViewModel
    {
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
        /// данные о роли
        /// </summary>
        public UserRolesModel UserRoleData { get; set; }

        /// <summary>
        /// Service data
        /// </summary>
        public UserServiceData ServiceData { get; set; }

        /// <summary>
        /// ошибка
        /// </summary>
        public ErrorModel Error { get; set; }
        #endregion
    }
}