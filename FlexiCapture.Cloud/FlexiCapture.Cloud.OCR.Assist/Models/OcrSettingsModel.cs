using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.OCR.Assist.Models
{
    /// <summary>
    /// ocr settings model
    /// </summary>
    public class OcrSettingsModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public OcrSettingsModel()
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
    //    "speedOcr": false,
    //"lookForBarcodes": true,
    //"analysisMode": "MixedDocument",
    //"printType": "Print",
    //"ocrLanguage": "English"
        /// <summary>
        /// speed ocr
        /// </summary>
        [JsonProperty("speedOcr")]
        public bool SpeedOcr { get; set; }


        /// <summary>
        /// look for barcodes
        /// </summary>
        [JsonProperty("lookForBarcodes")]
        public bool LookForBarcodes { get; set; }

        /// <summary>
        /// analysis mode
        /// </summary>
        [JsonProperty("analysisMode")]
        public string AnalysisMode { get; set; }

        /// <summary>
        /// print type
        /// </summary>
        [JsonProperty("printType")]
        public string PrintType { get; set; }

        /// <summary>
        /// lang
        /// </summary>
        [JsonProperty("ocrLanguage")]
        public string OcrLanguage { get; set; } 
        #endregion
    }
}
