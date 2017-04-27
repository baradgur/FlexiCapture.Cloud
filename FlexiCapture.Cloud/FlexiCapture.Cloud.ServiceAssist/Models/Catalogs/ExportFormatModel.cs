using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.ServiceAssist.Models.Catalogs
{
    /// <summary>
    /// export format VM
    /// </summary>
    public class ExportFormatModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public ExportFormatModel()
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
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }
        #endregion
    }
}