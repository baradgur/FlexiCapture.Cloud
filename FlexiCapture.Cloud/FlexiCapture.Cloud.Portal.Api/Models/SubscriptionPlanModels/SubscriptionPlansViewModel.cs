using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.SubscriptionPlanModels
{
    /// <summary>
    /// model for subscription plan
    /// </summary>
    public class SubscriptionPlansViewModel
    {
        #region fields
        /// <summary>
        /// plans list
        /// </summary>
        public List<SubscriptionPlanModel> Plans { get; set; }
        /// <summary>
        /// plan states
        /// </summary>
        public List<SubscriptionPlanStateModel> States { get; set; }
        /// <summary>
        /// plan types
        /// </summary>
        public List<SubscriptionPlanTypeModel> Types { get; set; }
        #endregion

        #region constructors

        public SubscriptionPlansViewModel()
        {
            Plans = new List<SubscriptionPlanModel>();
            States = new List<SubscriptionPlanStateModel>();
            Types = new List<SubscriptionPlanTypeModel>();
        }
        #endregion
    }
}