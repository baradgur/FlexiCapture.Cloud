using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.StoreModels
{
    public class StoreModel
    {
        /// <summary>
        /// user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// service id
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// state
        /// </summary>
        public bool State { get; set; }
    }
}