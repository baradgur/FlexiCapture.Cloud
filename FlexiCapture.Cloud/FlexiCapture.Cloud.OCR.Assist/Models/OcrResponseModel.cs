using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.OCR.Assist.Models
{
    public class OcrResponseModel
    {
        /// <summary>
        /// Job Url
        /// </summary>
        [JsonProperty("jobUrl")]
        
        public string JobUrl { get; set; }

        /// <summary>
        /// status
        /// </summary>
        
        [JsonProperty("status")]
        
        public string Status { get; set; }
    }
}
