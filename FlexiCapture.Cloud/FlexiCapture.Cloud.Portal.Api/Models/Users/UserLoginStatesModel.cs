using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Khingal.Models.Users
{
    public class UserLoginStatesModel
    {
        /// <summary>
        /// Id состояния учетной записи пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// название состояния учетной записи пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Краткое описание состояния учетной записи пользователя
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// полное описание состояния учетной записи пользователя
        /// </summary>
        public string FullDescription { get; set; }
    }
}