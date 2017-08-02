using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using FlexiCapture.Cloud.EmailAgent.Models;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.CommunicationModels;
using UserModel = FlexiCapture.Cloud.Portal.Api.Models.Users.UserModel;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class CommunicationController : ApiController
    {
        // GET: api/Communication
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(CommunicationHelper.GetCommunications());
            }
            catch (Exception exception)
            {
                return BadRequest("Error!");
            }
        }

        // GET: api/Communication
        public List<UserModel> Get(string userValue)
        {
            return UsersHelper.GetToUserData(userValue);
        }

        // POST: api/Communication
        public CommunicationModel Post([FromBody]CommunicationModel value)
        {
            return CommunicationHelper.SendCommunication(value);
        }

        // PUT: api/Communication/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Communication/5
        public void Delete(int id)
        {
        }
    }
}
