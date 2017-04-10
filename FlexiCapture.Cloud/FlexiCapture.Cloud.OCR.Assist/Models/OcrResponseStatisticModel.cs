using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.OCR.Assist.Models
{
    public class OcrResponseStatisticModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public OcrResponseStatisticModel()
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
        /// Files
        /// </summary>
        [JsonProperty("files")]
        public List<OcrResponseStatisticFileModel> Files { get; set; }
            /// <summary>
        /// count cahrs
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
