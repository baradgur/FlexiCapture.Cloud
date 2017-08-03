using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.OcrApiKeyModels
{
    public class OcrApiKeyModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Key { get; set; }
        public bool IsActive { get; set; }
    }
}