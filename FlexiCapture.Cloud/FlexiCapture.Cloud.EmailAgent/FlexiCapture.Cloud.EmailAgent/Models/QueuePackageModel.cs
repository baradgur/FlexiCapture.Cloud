using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FlexiCapture.Cloud.EmailAgent.Models
{
    [XmlRoot("QueuePackage")]
    public class QueuePackageModel
    {
        /// <summary>
        /// constructor
        /// </summary>
        public QueuePackageModel()
        {
            try
            {
                Task = new QueuePackageTaskModel();
                Emails = new List<EmailModel>();
            }
            catch (Exception)
            {
            }    
        }

        #region fields
        /// <summary>
        /// task
        /// </summary>
        public QueuePackageTaskModel Task { get; set; }
        /// <summary>
        /// emails
        /// </summary>
        [XmlArray("Emails")]
        [XmlArrayItem("Email")]
        public List<EmailModel> Emails { get; set; }
        #endregion
    }
}
