using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.Helpers.StoreHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.StoreModels;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class StoreController : ApiController
    {
        // GET api/store
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/store/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/store
        public void Post([FromBody]StoreModel model)
        {
            try
            {
                StoreHelper.SetServiceState(model);
            }
            catch (Exception)
            {
            }
        }

        // PUT api/store/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/store/5
        public void Delete(int id)
        {
        }
    }
}
