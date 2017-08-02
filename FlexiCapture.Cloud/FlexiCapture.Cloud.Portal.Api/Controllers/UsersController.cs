using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;
using FlexiCapture.Cloud.Portal.Api.Models.Users;
using FlexiCapture.Cloud.Portal.Api.Users;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/users
        public IHttpActionResult Get(int userId, int userRoleId)
        {
            try
            {
                return Ok(UsersHelper.GetToUsers(userId, userRoleId));
            }
            catch (Exception exception)
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
        public List<int> Delete(int id)
        {
           return UsersHelper.DeleteUser(id);
        }
    }
}
