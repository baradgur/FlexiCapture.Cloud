using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.Documents;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class DocumentsController : ApiController
    {
        // GET api/documents
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        // GET api/documents/5
        public string Get(int userId, int serviceId)
        {
            var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority)
                          + Configuration.VirtualPathRoot;
            return DocumentsHelper.GetDocumentsByUserServiceId(baseUrl, userId, serviceId);
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
                var sServiceId = HttpContext.Current.Request.Form.Get("serviceId");
                var sUserId = HttpContext.Current.Request.Form.Get("userId");
                string sProfile = HttpContext.Current.Request.Form.Get("profile");

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                ManageUserProfileModel model =serializer.Deserialize<ManageUserProfileModel>(sProfile);
                var serviceId = 0;
                var userId = 0;

                if (!string.IsNullOrEmpty(sServiceId)) serviceId = Convert.ToInt32(sServiceId);
                if (!string.IsNullOrEmpty(sUserId)) userId = Convert.ToInt32(sUserId);
                var hfc = HttpContext.Current.Request.Files;
                if (hfc.Count > 0)
                {
                        
                    //foreach (HttpPostedFile file in hfc)
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        var file = hfc[i];

                        Helpers.DocumentsHelpers.DocumentsHelper.ProcessFile(userId, serviceId, model, file, sProfile);
                        
                    }
                    //var file = hfc[0];
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/documents/5
        public string Delete(List<DocumentModel> models)
        {
            try
            {
                return DocumentsHelper.DeleteDocuments(models);
            }
            catch (Exception ex)
            {
                return "Error";
            }

        }
    }
}