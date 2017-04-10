using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.Documents
{
    public class DocumentModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public DocumentModel()
        {
            try
            {

            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region fields
        public int Id { get; set; }
        public string TaskId { get; set; }
        public string DateTime { get; set; }

        public double FileSizeBytes { get; set; }

        public double FileSize { get; set; }

        public string OriginalFileName { get; set; }

        public string Url { get; set; }

        public int StateId { get; set; }

        public string StateName { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; }
        #endregion
    }
}