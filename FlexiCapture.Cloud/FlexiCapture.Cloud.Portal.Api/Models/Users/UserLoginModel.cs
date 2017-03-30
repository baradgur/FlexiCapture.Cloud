using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.Users
{
    public class UserLoginModel
    {

        /// <summary>
        /// id логина
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// логин пользователя
        /// </summary>
        public string UserLogin { get; set; }

        /// <summary>
        /// пароль пользователя
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// дата регистрации пользователя
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// дата последней активности пользователя
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

       
        /// <summary>
        /// Id состояния учетной записи пользователя
        /// </summary>
        public int UserLoginStateId { get; set; }

        /// <summary>
        /// название состояния учетной записи пользователя
        /// </summary>
        public string UserLoginStateName { get; set; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }
    }
}