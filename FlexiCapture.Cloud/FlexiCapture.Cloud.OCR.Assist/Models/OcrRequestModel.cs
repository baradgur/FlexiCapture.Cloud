using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.OCR.Assist.Models
{
    
    public class OcrRequestModel
    {
        /// <summary>
        ///apiKey 
        /// </summary>
        
        [JsonProperty("apiKey")]
        public string ApiKey {get; set; }

        /// <summary>
        ///notifyUrl
        /// </summary>
        [JsonProperty("notifyUrl")]
        public string NotifyUrl { get; set; }

        /// <summary>
        /// cleanup settings
        /// </summary>
        [JsonProperty("cleanupSettings")]
        public CleanupSettingsModel CleanupSettings { get; set; }

        /// <summary>
        /// ocr settings
        /// </summary>
        [JsonProperty("ocrSettings")]
        public OcrSettingsModel OcrSettings { get; set; }

        /// <summary>
        /// output settings
        /// </summary>
        [JsonProperty("outputSettings")]
        public OutputSettingsModel OutputSettings { get; set; }

    }
}
