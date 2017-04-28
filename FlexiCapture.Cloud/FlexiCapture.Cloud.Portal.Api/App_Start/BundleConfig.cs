using System.Web;
using System.Web.Optimization;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.CryptHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.ManageUserHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.ServiceSettingsHelper;

namespace FlexiCapture.Cloud.Portal.Api
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            //string p = PasswordHelper.Crypt.EncryptString("xSmHXUg62rq7");
            //ManageUserHelper.GetToManageUserProfile(1);
            //string hash = MD5Helper.GetMD5HashFromFile("d://164305872312.kwm");
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
