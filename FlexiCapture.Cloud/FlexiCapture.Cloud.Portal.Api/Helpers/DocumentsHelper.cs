using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Razor;

namespace FlexiCapture.Cloud.Portal.Api.Helpers
{
    public static class DocumentsHelper
    {
        public static FileStream GetDocumentFromFileSystem(DB.Documents document)
        {
            return new FileStream(Path.Combine(HostingEnvironment.MapPath("~/"), document.Path),
                FileMode.Open, FileAccess.Read);
        }
    }
}