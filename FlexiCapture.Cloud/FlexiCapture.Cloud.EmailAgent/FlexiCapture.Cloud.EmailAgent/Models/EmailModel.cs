using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.EmailAgent.Models
{
    [Serializable]
    public class EmailModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public EmailModel()
        {
            try
            {
                Task = new QueuePackageTaskModel();
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region fields
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// from email
        /// </summary>
        public string FromEmail { get; set; }

        /// <summary>
        /// to emails 
        /// </summary>
        public string ToEmails { get; set; }

        /// <summary>
        /// subject email
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// content of email
        /// </summary>
        public EmailContentModel EmailContent { get; set; }

        /// <summary>
        /// id of type email
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// string content
        /// </summary>
        public string EmailContentLine { get; set; }

        /// <summary>
        /// task
        /// </summary>
        public QueuePackageTaskModel Task { get; set; }
        #endregion
    }
}
