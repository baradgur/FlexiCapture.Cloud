using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.Helpers.UserProfileHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class UserRegistrationController : ApiController
    {
        // GET api/userregistration
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/userregistration/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/userregistration
        public string Post(UserProfileModel model)
        {
            return UserProfileHelper.RegistrationUser(model);
        }

        // PUT api/userregistration/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/userregistration/5
        public void Delete(int id)
        {
        }
    }
}
