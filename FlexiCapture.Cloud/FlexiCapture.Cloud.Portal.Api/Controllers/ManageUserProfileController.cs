using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.Helpers.ManageUserHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.UserProfileHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class ManageUserProfileController : ApiController
    {
        // GET api/manageuserprofile
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/manageuserprofile/5
        public string Get(int profileId, int serviceId)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(ManageUserProfileHelper.GetToUserProfileById(profileId,serviceId));
        }

       

        // POST api/manageuserprofile
        public void Post([FromBody]string value)
        {
        }

        // PUT api/manageuserprofile/5
        public string Put(UserProfileModel model)
        {
            return UserProfileHelper.UpdateUserProfile(model);
        }

        // DELETE api/manageuserprofile/5
        public void Delete(int id)
        {
        }
    }
}
