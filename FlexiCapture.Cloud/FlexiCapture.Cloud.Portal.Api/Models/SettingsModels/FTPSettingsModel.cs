using FlexiCapture.Cloud.Portal.Api.Models.Errors;

namespace FlexiCapture.Cloud.Portal.Api.Models.SettingsModels
{
    /// <summary>
    /// 
    /// </summary>
    public class FTPSettingsModel
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
        /// user name for FTP settings
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// host name
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// port for ftp connection default 21
        /// </summary>
        public int? Port { get; set; }
        /// <summary>
        /// password for ftp connection
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// path for ftp connection
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// use SSL bool param for ftp conection
        /// </summary>
        public bool UseSSL { get; set; }
        public bool DeleteFile { get; set; }
        /// <summary>
        /// Enabling usage of that setting
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// model providing errors to user
        /// </summary>
        public ErrorModel Error { get; set; }
    }
}