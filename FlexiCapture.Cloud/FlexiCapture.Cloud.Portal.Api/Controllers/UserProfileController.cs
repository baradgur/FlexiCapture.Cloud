using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.Helpers.ManageUserHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class UserProfileController : ApiController
    {
        // GET api/userprofile
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/userprofile/5
        public string Get(int userId)
        {
            return ManageUserProfileHelper.GetToAllUserProfiles(userId);
        }

        // POST api/userprofile
        public void Post([FromBody]ManageUserProfileModel data)
        {
            string n = "";
        }

        // PUT api/userprofile/5
        public void Put([FromBody]ManageUserProfileModel data)
        {
            DBHelpers.ManageUserProfileHelper.UpdateUserProfile(data);
        }

        // DELETE api/userprofile/5
        public void Delete(int id)
        {
        }
    }
}
