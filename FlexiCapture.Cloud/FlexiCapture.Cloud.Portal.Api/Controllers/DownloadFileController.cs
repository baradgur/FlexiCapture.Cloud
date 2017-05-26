using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.DB;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class DownloadFileController : ApiController
    {
        public HttpResponseMessage Get(int documentId)
        {

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            Documents document = DBHelpers.DocumentsHelper.GetDocumentsById(documentId);
            var stream = Helpers.DocumentsHelper.GetDocumentFromFileSystem(document);

            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = document.OriginalFileName;
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.Add("x-filename", document.OriginalFileName); //We will use this below

            return result;
        }
    }
}
