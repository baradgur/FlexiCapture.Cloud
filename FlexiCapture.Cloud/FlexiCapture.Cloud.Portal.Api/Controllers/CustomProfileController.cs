using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class CustomProfileController : ApiController
    {
        // GET api/customprofile
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/customprofile/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/customprofile
        public string  Post([FromBody]ManageUserProfileModel data)
        {
            return DBHelpers.ManageUserProfileHelper.CreateNewDefaultProfile(data);
        }

        // PUT api/customprofile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/customprofile/5
        public void Delete(int id)
        {
        }
    }
}
