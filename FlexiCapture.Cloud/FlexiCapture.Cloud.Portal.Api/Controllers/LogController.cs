using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class LogController : ApiController
    {
        // GET: api/Log
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(LogHelper.GetToLogs());
            }
            catch (Exception exception)
            {
                return BadRequest("Error!");

            }
        }

        // GET: api/Log/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Log
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Log/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Log/5
        public void Delete(int id)
        {
        }
    }
}
