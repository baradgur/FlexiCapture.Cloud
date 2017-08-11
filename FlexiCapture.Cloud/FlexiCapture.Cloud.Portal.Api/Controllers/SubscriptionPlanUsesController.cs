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
    public class SubscriptionPlanUsesController : ApiController
    {
        // GET: api/SubscriptionPlanUses
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SubscriptionPlanUses/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SubscriptionPlanUses
        public SubscriptionPlanUseModel Post([FromBody]SubscriptionPlanUseModel value)
        {
            return SubscriptionPlansHelper.AddPlanUse(value);
        }

        // PUT: api/SubscriptionPlanUses/5
        public SubscriptionPlanUseModel Put([FromBody]SubscriptionPlanUseModel value)
        {
            return SubscriptionPlansHelper.UpdatePlanUse(value);
        }

        // DELETE: api/SubscriptionPlanUses/5
        public void Delete(int id)
        {
        }
    }
}
