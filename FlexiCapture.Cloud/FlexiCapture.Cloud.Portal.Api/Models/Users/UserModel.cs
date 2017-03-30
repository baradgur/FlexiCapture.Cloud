namespace FlexiCapture.Cloud.Portal.Api.Models.Users
{
    public class UserModel
    {
        /// <summary>
        /// идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

       
        /// <summary>
        /// идентификатор роли пользователя
        /// </summary>
        public int UserRoleId { get; set; }

        /// <summary>
        /// имя пользователя
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        /// фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// дата последней активности
        /// </summary>
        public string LastActivityDate { get; set; }

        /// <summary>
        /// company name
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        public string PhoneNumber { get; set; }
       /// <summary>
       /// constructor
       /// </summary>
        public UserModel()
        {
        }

    }
}