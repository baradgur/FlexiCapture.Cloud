using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.OCR.Assist.Models
{
    public class InputFileModel
    {
        #region constructor
        /// <summary>
        /// constuctor
        /// </summary>
        public InputFileModel()
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
        //        “name”: “string”,
//“password”: “string”,
//“inputUrl”: ”string”,
//“inputBlob”: “string”,
//“inputType”: “string”,
//“postFix”: “string”

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// password
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
        /// <summary>
        /// input url
        /// </summary>
        [JsonProperty("inputUrl")]
        public string InputUrl { get; set; }
        /// <summary>
        /// input blob
        /// </summary>
        [JsonProperty("inputBlob")]
        public string InputBlob { get; set; }
        /// <summary>
        /// input type
        /// </summary>
        [JsonProperty("inputType")]
        public string InputType { get; set; }
        
        /// <summary>
        /// post fix
        /// </summary>
        [JsonProperty("postFix")]
        public string PostFix { get; set; }

        #endregion
    }
}
