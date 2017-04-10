using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace FlexiCapture.Cloud.OCR.Assist.Models
{
    /// <summary>
    /// download model
    /// </summary>
    public class DownloadModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public DownloadModel()
        {
            try
            {
                Files = new List<DownloadFilesModel>();
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region fields
        /// <summary>
        /// files
        /// 
        /// </summary>
        [JsonProperty("")]
        public List<DownloadFilesModel> Files { get; set; } 
            
        #endregion
    }
}
