using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.ServiceAssist.Models.Errors
{
    public class ErrorModel
    {
        /// <summary>
        /// имя ошибки
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// код ошибки
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// краткое описание ошибки
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// полное описание ошибки
        /// </summary>
        public string FullDescription { get; set; }
    }
}