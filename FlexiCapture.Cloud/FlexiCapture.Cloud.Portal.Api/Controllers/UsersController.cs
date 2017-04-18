using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;

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
        public void Post([FromBody]string value)
        {
        }

        // PUT api/users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/users/5
        public void Delete(int id)
        {
        }
    }
}
