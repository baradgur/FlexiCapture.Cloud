using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.Users
{
    public class UserServiceData
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public UserServiceData()
        {
            try
            {
                SingleFileConversionService = true;
            }
            catch (Exception)
            {
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public bool SingleFileConversionService { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool FTPFileConversionService { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool EmailAttachmentFileConversionService { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool BatchFileConversionService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool OnlineWebOcrApiService { get; set; }
    }
}