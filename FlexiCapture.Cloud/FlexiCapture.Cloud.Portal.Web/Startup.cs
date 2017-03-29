using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FlexiCapture.Cloud.Portal.Web.Startup))]
namespace FlexiCapture.Cloud.Portal.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
