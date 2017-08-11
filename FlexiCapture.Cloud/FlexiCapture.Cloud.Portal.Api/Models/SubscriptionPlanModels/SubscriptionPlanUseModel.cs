using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.SubscriptionPlanModels
{
    /// <summary>
    /// model for subscription plan use
    /// </summary>
    public class SubscriptionPlanUseModel
    {
        #region fields
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// plan id
        /// </summary>
        public int? NextSubscriptionPlanId { get; set; }
        /// <summary>
        /// plan name
        /// </summary>
        public string NextSubscriptionPlanName { get; set; }

        /// <summary>
        /// plan id
        /// </summary>
        public int SubscriptionPlanId { get; set; }
        /// <summary>
        /// plan name
        /// </summary>
        public string SubscriptionPlanName { get; set; }

        /// <summary>
        /// plan type id
        /// </summary>
        public int SubscriptionPlanTypeId { get; set; }

        /// <summary>
        /// use plan state Id
        /// </summary>
        public int SubscriptionPlanUseStateId { get; set; }
        /// <summary>
        /// use plan state name
        /// </summary>
        public string SubscriptionPlanUseStateName { get; set; }
        /// <summary>
        /// date, when use of the plan ends
        /// </summary>
        public DateTime? DateExpiry { get; set; }
        /// <summary>
        /// pages to be used in this plan use
        /// </summary>
        public int PagesCount { get; set; }
        /// <summary>
        /// pages can be used in this plan
        /// </summary>
        public int PagesMax { get; set; }
        /// <summary>
        /// cost per page
        /// </summary>
        public int CostPerPage { get; set; }
        /// <summary>
        /// is default
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// amount of pages used above the pageInInterval by alternative price
        /// </summary>
        public int? AtlPagesCount { get; set; }
        /// <summary>
        /// amount of paid pages used above the pageInInterval
        /// </summary>
        public int? PaidAtlPagesCount { get; set; }

        /// <summary>
        /// user id
        /// </summary>
        public int UserId { get; set; }
        #endregion

        #region constructors

        public SubscriptionPlanUseModel()
        {
        }
        #endregion
    }
}