using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.Helpers.StatisticHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.StatisticModels;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class StatisticController : ApiController
    {
        // GET: api/Statistic
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Statistic/5
        public string Get(int id)
        {
            return StatisticHelper.GetToDefaultSettings();
        }

        // POST: api/Statistic
        public string Post([FromBody]StatisticRequestModel model)
        {
            return StatisticHelper.ShowStatistic(model);
        }

        // PUT: api/Statistic/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Statistic/5
        public void Delete(int id)
        {
        }
    }
}
