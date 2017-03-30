using System.Collections.Generic;
using Khingal.Models.Users;

namespace FlexiCapture.Cloud.Portal.Api.Models.Users
{
    public class UsersViewModel
    {
        #region fields
        /// <summary>
        /// данные пользователей
        /// </summary>
        public List<UserViewModel> UsersData { get; set; }

        

        /// <summary>
        /// данные о ролях
        /// </summary>
        public List<UserRolesModel> UserRolesData { get; set; }

        /// <summary>
        /// данные о состояниях учетной записи
        /// </summary>
        public List<UserLoginStatesModel> LoginStatesData { get; set; }
        #endregion
    }
}