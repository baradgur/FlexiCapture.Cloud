using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.Helpers.DocumentsHelpers;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class DocumentsController : ApiController
    {
        // GET api/documents
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/documents/5
        public string Get(int userId, int serviceId)
        {
            String baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority)
 + Configuration.VirtualPathRoot;
            return DBHelpers.DocumentsHelper.GetDocumentsByUserServiceId(baseUrl,userId,serviceId);
        }

        public string Get(int documentId)
        {
            return "value";
        }

        // POST api/documents
        public IHttpActionResult Post()
        {
            try
            {
                string sServiceId = System.Web.HttpContext.Current.Request.Form.Get("serviceId");
                string sUserId = System.Web.HttpContext.Current.Request.Form.Get("userId");

                int serviceId = 0;
                int userId = 0;

                if (!string.IsNullOrEmpty(sServiceId)) serviceId = Convert.ToInt32(sServiceId);
                if (!string.IsNullOrEmpty(sUserId)) userId = Convert.ToInt32(sUserId);
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                if (hfc.Count > 0)
                {
                    HttpPostedFile file = hfc[0];
                    DocumentsHelper.ProcessFile(userId, serviceId, file);
                    return Ok("File Upload Completely");

                }
                return BadRequest("No files");
            }
            catch (Exception exception)
            {
                return BadRequest("Bad request");
            }
        }

        // PUT api/documents/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/documents/5
        public void Delete(int id)
        {
        }
    }
}
