using System.Web;
using System.Web.Mvc;

namespace FlexiCapture.Cloud.Portal.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
