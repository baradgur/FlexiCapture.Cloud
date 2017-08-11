using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.SubscriptionPlanModels;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class SubscriptionPlansController : ApiController
    {
        // GET: api/SubscriptionPlans
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(SubscriptionPlansHelper.GetSubscriptionPlansView());
            }
            catch (Exception exception)
            {
                return BadRequest();
            }
        }

        // GET: api/SubscriptionPlans/5
        public SubscriptionPlanUsesViewModel Get(int id)
        {
            return SubscriptionPlansHelper.GetSubscriptionPlanUsesView(id);
        }

        // POST: api/SubscriptionPlans
        public SubscriptionPlanModel Post([FromBody]SubscriptionPlanModel value)
        {
            return SubscriptionPlansHelper.AddPlan(value);
        }

        // PUT: api/SubscriptionPlans/5
        public SubscriptionPlanModel Put([FromBody]SubscriptionPlanModel value)
        {
            return SubscriptionPlansHelper.UpdatePlan(value);
        }

        // DELETE: api/SubscriptionPlans/5
        public void Delete(int id)
        {
        }
    }
}
