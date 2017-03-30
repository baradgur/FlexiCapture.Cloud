namespace FlexiCapture.Cloud.Portal.Api.Models.Users
{
    public class UserRolesModel
    {
        #region fields
        /// <summary>
        /// идентификатор роли
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// название роли
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// краткое описание роли
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// идентификатор типа роли пользователя(групповая или нет)
        /// </summary>
        public int UserRoleTypeId { get; set; }
        #endregion
    }
}