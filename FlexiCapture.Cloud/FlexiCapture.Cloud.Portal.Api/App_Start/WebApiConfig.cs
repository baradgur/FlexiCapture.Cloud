using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.Preflight;

namespace FlexiCapture.Cloud.Portal.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //LogHelper.AddLog("ADD CORS");
           // config.EnableCors(cors);
           // LogHelper.AddLog("ENABLE CORS");
           // LogHelper.AddLog("ADD M HANDLERS");
           // config.MessageHandlers.Add(new PreflightRequestsHandler());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
