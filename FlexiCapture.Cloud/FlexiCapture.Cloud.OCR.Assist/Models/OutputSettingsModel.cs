using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.OCR.Assist.Models
{
    /// <summary>
    /// output settings model
    /// </summary>
    public class OutputSettingsModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public OutputSettingsModel()
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
        /// exportFormat
        /// </summary>
        [JsonProperty("exportFormat")]
        public string ExportFormat { get; set; }
        #endregion
    }
}
