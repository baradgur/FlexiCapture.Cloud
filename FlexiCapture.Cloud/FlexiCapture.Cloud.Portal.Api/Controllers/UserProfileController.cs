using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.Helpers.ManageUserHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.GeneralModels;
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
        public string Get(int userId, int serviceId)
        {
            return ManageUserProfileHelper.GetToAllUserProfiles(userId, serviceId);
        }

        // POST api/userprofile
        public string Post(NewProfileModel data)
        {

            return DBHelpers.ManageUserProfileHelper.CreateNewProfile(data);
        }

        // PUT api/userprofile/5
        public string Put([FromBody]ManageUserProfileModel data)
        {
           return DBHelpers.ManageUserProfileHelper.UpdateUserProfile(data);
        }

        // DELETE api/userprofile/5
        public void Delete(int id)
        {
        }
    }
}
