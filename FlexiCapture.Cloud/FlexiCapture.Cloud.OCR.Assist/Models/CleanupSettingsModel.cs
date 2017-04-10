using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.OCR.Assist.Models
{
    /// <summary>
    /// cleanup settings model
    /// </summary>
    public class CleanupSettingsModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public CleanupSettingsModel()
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
//          "deskew": true,
//    "removeGarbage": true,
//    "removeTexture": true,
//    "splitDualPage": true,
//    "rotationType": "NoRotation",
//    "outputFormat": "string",
//    "resolution": "string",
//    "jpegQuality": "string"

        /// <summary>
        /// deskew
        /// </summary>
        [JsonProperty("deskew")]
        public bool Deskew { get; set; }

        /// <summary>
        /// removeGarbage
        /// </summary>
        [JsonProperty("removeGarbage")]
        public bool RemoveGarbage { get; set; }

        /// <summary>
        /// removeTexture
        /// </summary>
        [JsonProperty("removeTexture")]
        public bool RemoveTexture { get; set; }

        /// <summary>
        /// splitDualPage
        /// </summary>
        [JsonProperty("splitDualPage")]
        public bool SplitDualPage { get; set; }

        /// <summary>
        /// rotationType
        /// </summary>
        [JsonProperty("rotationType")]
        public string RotationType { get; set; }

        /// <summary>
        /// outputFormat
        /// </summary>
        [JsonProperty("outputFormat")]
        public string OutputFormat { get; set; }

        /// <summary>
        /// resolution
        /// </summary>
        [JsonProperty("resolution")]
        public string Resolution { get; set; }


        /// <summary>
        /// jpegQuality
        /// </summary>
        [JsonProperty("jpegQuality")]
        public string JpegQuality { get; set; }
        #endregion
    }
}
