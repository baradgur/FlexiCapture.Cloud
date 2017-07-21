using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.EmailAgent.Models
{
    [Serializable]
    public class EmailAttachmentModel
    {
       
        #region fields

        /// <summary>
        /// path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// OriginalName
        /// </summary>
        public string OriginalName { get; set; }
        #endregion
    }
}
