using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.Helpers.RestoreHelpers;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class RestoreDataController : ApiController
    {
        // GET api/restoredata
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/restoredata/5
        public string Get(string email)
        {
            return RestoreHelper.RestoreUserPassword(email);
        }

        // POST api/restoredata
        public void Post([FromBody]string data)
        {
            
        }

        // PUT api/restoredata/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/restoredata/5
        public void Delete(int id)
        {
        }
    }
}
