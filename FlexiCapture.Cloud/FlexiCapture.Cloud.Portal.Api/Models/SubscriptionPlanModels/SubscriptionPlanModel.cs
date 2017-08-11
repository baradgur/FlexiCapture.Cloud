using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.SubscriptionPlanModels
{
    /// <summary>
    /// model for subscription plan
    /// </summary>
    public class SubscriptionPlanModel
    {
        #region fields
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// plan name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// auto-renewal type (one-time purcahse, monthly/annual subscription)
        /// </summary>
        public int SubscriptionPlanTypeId { get; set; }
        /// <summary>
        /// auto-renewal type name
        /// </summary>
        public string SubscriptionPlanTypeName { get; set; }
        /// <summary>
        /// plan state
        /// </summary>
        public int SubscriptionPlanStateId { get; set; }
        /// <summary>
        /// plan state name
        /// </summary>
        public string SubscriptionPlanStateName { get; set; }
        /// <summary>
        /// how many days per plan (for one-time purchase plans)
        /// </summary>
        public int? Expiration { get; set; }
        /// <summary>
        /// cost per page in cents
        /// </summary>
        public int CostPerPage { get; set; }
        /// <summary>
        /// quantity of pages in a plan
        /// </summary>
        public int PagesInInterval { get; set; }
        /// <summary>
        /// default plans can be used only once
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// alternative cost per page in cents, if more pages needed, than are given in PagesInInterval
        /// </summary>
        public int? AltCostPerPage { get; set; }
        #endregion

        #region constructors

        public SubscriptionPlanModel()
        {
        }
        #endregion
    }
}