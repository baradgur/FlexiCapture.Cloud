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

        public OcrResponseModel()
        {
            try
            {
                Download = new List<DownloadFilesModel>();
                Statistics =new OcrResponseStatisticModel();
            }
            catch (Exception)
            {
            }
        }
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

        [JsonProperty("download")]
        public List<DownloadFilesModel> Download { get; set; }

        [JsonProperty("statistics")]
        public OcrResponseStatisticModel Statistics { get; set; }


    }
}
