using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;
using Khingal.Models.Users;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/users
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(UsersHelper.GetToUsers());
            }
            catch (Exception)
            {
                return BadRequest("Error!");

            }
        }

        // GET api/users/5
        public string Get(int userId)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(UsersHelper.GetToUserData(userId));
        }

        // POST api/users
        public string Post(UserViewModel model)
        {
            return UsersHelper.AddUserAdmin(model);
        }

        // PUT api/users/5
        public string Put(UserViewModel model)
        {
            return UsersHelper.UpdateUser(model);
        }

        // DELETE api/users/5
        public void Delete(int id)
        {
        }
    }
}
