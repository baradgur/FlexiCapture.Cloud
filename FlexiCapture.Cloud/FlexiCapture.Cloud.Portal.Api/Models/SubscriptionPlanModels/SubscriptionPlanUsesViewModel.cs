using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.SubscriptionPlanModels
{
    /// <summary>
    /// model for subscription plan
    /// </summary>
    public class SubscriptionPlanUsesViewModel
    {
        #region fields
        /// <summary>
        /// plans list
        /// </summary>
        public List<SubscriptionPlanModel> Plans { get; set; }
        /// <summary>
        /// plan states
        /// </summary>
        public List<SubscriptionPlanUseModel> PlanUses { get; set; }
        #endregion

        #region constructors

        public SubscriptionPlanUsesViewModel()
        {
            Plans = new List<SubscriptionPlanModel>();
            PlanUses = new List<SubscriptionPlanUseModel>();
        }
        #endregion
    }
}