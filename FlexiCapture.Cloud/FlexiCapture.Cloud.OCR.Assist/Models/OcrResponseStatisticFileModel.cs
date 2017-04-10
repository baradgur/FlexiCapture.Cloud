using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.OCR.Assist.Models
{
    public class OcrResponseStatisticFileModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public OcrResponseStatisticFileModel()
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
        /// file name
        /// </summary>
        [JsonProperty("fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// characters count
        /// </summary>
        [JsonProperty("totalCharacters")]
        public int TotalCharacters { get; set; }

        /// <summary>
        /// uncertainCharacters
        /// </summary>
        [JsonProperty("uncertainCharacters")]
        public int UncertainCharacters { get; set; }

        [JsonProperty("pagesArea")]
        public int PagesArea { get; set; }


        #endregion
    }
}
