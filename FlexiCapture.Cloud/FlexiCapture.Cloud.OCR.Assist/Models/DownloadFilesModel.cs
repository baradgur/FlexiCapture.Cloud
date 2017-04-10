using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.OCR.Assist.Models
{
    /// <summary>
    /// down files
    /// </summary>
    public class DownloadFilesModel
    {
        public DownloadFilesModel()
        {
            try
            {

            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// url
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }
        /// <summary>
        /// output format
        /// </summary>
        [JsonProperty("outputFormat")]
        public string OutputFormat { get; set; }
    }
}
