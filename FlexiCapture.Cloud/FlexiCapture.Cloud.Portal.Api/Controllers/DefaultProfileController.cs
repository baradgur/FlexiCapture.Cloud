using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class DefaultProfileController : ApiController
    {
        // GET api/defaultprofile
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/defaultprofile/5
        public string Get(int profileId, int newProfileId, int serviceId)
        {
            DefaultProfileHelper.SetNewDefaultProfile(profileId,newProfileId,serviceId);
            return "";
        }

        // POST api/defaultprofile
        public void Post([FromBody]int id)
        {
        }

        // PUT api/defaultprofile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/defaultprofile/5
        public void Delete(int id)
        {
        }
    }
}
