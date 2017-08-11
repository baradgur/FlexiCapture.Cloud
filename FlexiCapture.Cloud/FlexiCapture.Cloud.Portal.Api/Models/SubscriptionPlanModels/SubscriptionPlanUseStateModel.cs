using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.SubscriptionPlanModels
{
    /// <summary>
    /// model for subscription plan
    /// </summary>
    public class SubscriptionPlanUseStateModel
    {
        #region fields
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// plan type name
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region constructors

        public SubscriptionPlanUseStateModel()
        {
        }
        #endregion
    }
}