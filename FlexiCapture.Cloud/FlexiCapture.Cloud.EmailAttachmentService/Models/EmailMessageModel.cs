using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.EmailAttachmentService.Models
{
    public class EmailMessageModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public EmailMessageModel()
        {
            try
            {

            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region
        /// <summary>
        /// sender email
        /// </summary>
        public string SenderEmail { get; set; }

        /// <summary>
        /// receiver email
        /// </summary>
        public string ReceiverEmail { get; set; }

        /// <summary>
        /// date email
        /// </summary>
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// body
        /// </summary>
        public string Body { get; set; }
        #endregion
    }
}
