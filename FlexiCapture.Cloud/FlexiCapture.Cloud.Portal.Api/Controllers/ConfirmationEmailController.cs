using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class ConfirmationEmailController : ApiController
    {
        // GET api/confirmationemail
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/confirmationemail/5
        public string Get(Guid guid)
        {
           return  ConfirmationEmailHelper.ConfirmationEmail(guid);
        }

        // POST api/confirmationemail
        public void Post([FromBody]string value)
        {
        }

        // PUT api/confirmationemail/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/confirmationemail/5
        public void Delete(int id)
        {
        }
    }
}
