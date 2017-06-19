using System;
using System.Collections.Generic;
using System.IO;
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
            var file = Helpers.DocumentsHelper.GetDocumentFromFileSystem(document);

            //result.Content = new StreamContent(stream);
            //result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            //result.Content.Headers.ContentDisposition.FileName = document.OriginalFileName;
            //result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            //result.Content.Headers.Add("x-filename", document.OriginalFileName); //We will use this below
            MemoryStream ms = new MemoryStream();
            byte[] bytes = new byte[file.Length];
            file.Read(bytes, 0, (int)file.Length);
            ms.Write(bytes, 0, (int)file.Length);
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            httpResponseMessage.Content = new ByteArrayContent(bytes.ToArray());
            httpResponseMessage.Content.Headers.Add("X-File-Name", document.OriginalFileName);
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = document.OriginalFileName;
            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            return httpResponseMessage;
        }
    }
}
