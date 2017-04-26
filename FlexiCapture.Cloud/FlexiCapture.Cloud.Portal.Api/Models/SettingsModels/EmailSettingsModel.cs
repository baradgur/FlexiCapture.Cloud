using FlexiCapture.Cloud.Portal.Api.Models.Errors;

namespace FlexiCapture.Cloud.Portal.Api.Models.SettingsModels
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailSettingsModel
    {   
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// user id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Account name for Email settings
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// host name
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// port for Email connection
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// password for Email connection
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Email for Email connection
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// use SSL bool param for Email conection
        /// </summary>
        public bool UseSSL { get; set; }
        /// <summary>
        /// model providing errors to user
        /// </summary>
        public ErrorModel Error { get; set; }
    }
}