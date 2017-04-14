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
                ResultDocuments = new List<DocumentModel>();
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region fields
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// task id
        /// </summary>
        public string TaskId { get; set; }
        /// <summary>
        /// date
        /// </summary>
        public string DateTime { get; set; }

        public double FileSizeBytes { get; set; }
        /// <summary>
        /// file size
        /// </summary>
        public double FileSize { get; set; }

        /// <summary>
        /// original file name
        /// </summary>
        public string OriginalFileName { get; set; }

        /// <summary>
        /// url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// state id
        /// </summary>
        public int StateId { get; set; }

        /// <summary>
        /// state name
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// type id
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// type name
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// result docs
        /// </summary>
        public List<DocumentModel> ResultDocuments { get; set; } 
        #endregion
    }
}