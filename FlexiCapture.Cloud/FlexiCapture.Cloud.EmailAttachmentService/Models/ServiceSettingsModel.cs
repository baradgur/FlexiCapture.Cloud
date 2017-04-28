using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.EmailAttachmentService.Models
{
    public enum ReceiveType { IMAP, POP3 }
    /// <summary>
    /// configuration settings model
    /// </summary>
    [Serializable]
    public class ServiceSettingsModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public ServiceSettingsModel()
        {
            try
            {
                Credentials = new EmailCredentials();
                AdminCredentials = new EmailCredentials();
                ReceiveType = ReceiveType.IMAP;
            }
            catch (Exception)
            {
            }
        }
        #endregion

        
        #region fields

        /// <summary>
        /// agent name
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        /// agent email
        /// </summary>
        public string AgentEmail { get; set; }

        /// <summary>
        /// receive type
        /// </summary>
        public ReceiveType ReceiveType { get; set; }
        /// <summary>
        /// IMAP Settings
        /// </summary>
        public IMAPConnectionProtocolModel ImapSettings { get; set; }

        /// <summary>
        /// ADMIN SMTP Settings
        /// </summary>
        public IMAPConnectionProtocolModel AdminSMTPSettings { get; set; }
        /// <summary>
        /// SMTP Settings
        /// </summary>
        public IMAPConnectionProtocolModel SMTPSettings { get; set; }

        /// <summary>
        /// IMAP Settings
        /// </summary>
        public POPConnectionProtocolModel POP3Settings { get; set; }
        /// <summary>
        /// SSL connection?
        /// </summary>
        public bool UsingSsl { get; set; }
        /// <summary>
        /// credentials
        /// </summary>
        public EmailCredentials Credentials { get; set; }


        /// <summary>
        /// admin credentials
        /// </summary>
        public EmailCredentials AdminCredentials { get; set; }
        #endregion
    }
}
