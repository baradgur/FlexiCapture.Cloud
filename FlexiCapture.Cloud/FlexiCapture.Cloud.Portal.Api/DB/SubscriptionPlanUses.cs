//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlexiCapture.Cloud.Portal.Api.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class SubscriptionPlanUses
    {
        public int Id { get; set; }
        public int SubscriptionPlanId { get; set; }
        public int UserId { get; set; }
        public int PagesCount { get; set; }
        public Nullable<System.DateTime> DateExpiry { get; set; }
        public Nullable<int> NextSubscriptionPlanId { get; set; }
        public int SubscriptionPlanUseStateId { get; set; }
        public Nullable<int> AltPagesCount { get; set; }
        public Nullable<int> PaidAltPagesCount { get; set; }
    
        public virtual SubscriptionPlans SubscriptionPlans { get; set; }
        public virtual SubscriptionPlans SubscriptionPlans1 { get; set; }
        public virtual SubscriptionPlanUseStates SubscriptionPlanUseStates { get; set; }
        public virtual Users Users { get; set; }
    }
}
