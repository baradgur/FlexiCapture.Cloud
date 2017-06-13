using Newtonsoft.Json;

namespace FlexiCapture.Cloud.OCR.Assist.Models
{
    public class OcrError
    {
        [JsonProperty("code")]
        public string ErrorName { get; set; }
        [JsonProperty("message")]
        public string ErrorMessage { get; set; }
    }
}