using System.Web.Optimization;

namespace FlexiCapture.Cloud.Portal.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = true;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-dialog.min.js",
                "~/Scripts/bootstrap-notify.js",
                "~/Scripts/BootstrapMenu.min.js",
                "~/Scripts/lightbox.js",
                "~/Scripts/validator.min.js",
                "~/Scripts/validator.js"
                // "~/Scripts/bootstrap-datetimepicker.min.js"
                //
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-dialog.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/metisMenu.min.css",
                "~/Content/site.css",
                "~/Content/sb-admin-2.css",
                "~/Content/animate.css",
                "~/Content/bootstrap-table.css",
                "~/Content/lightbox.css",
                "~/Scripts/chat/chat.css",
                "~/Content/chat_list.css",
                "~/Content/chk-multi-select.css"
                //"~/Content/bootstrap-datetimepicker.min.css"
                ));

            //additional byndles for project
            bundles.Add(new ScriptBundle("~/bundles/sbadmin").Include(
                "~/Scripts/sb-admin-2.js",
                "~/Scripts/metisMenu.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table").Include(
                "~/Scripts/bootstrap-table.js",
                "~/Scripts/bootstrap-table-en-US.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-ui-router.js",
                "~/Scripts/angular-messages.js",
                "~/Scripts/angular-cookies.js",
                "~/Scripts/angular-idle.js",
                "~/Scripts/angular-route.min.js",
                "~/Scripts/angular-ui-router-title.js",
                "~/Scripts/spin.js",
                "~/Scripts/angular-spinner.js",
                "~/Scripts/ui-bootstrap-tpls-2.1.3.min.js",
                "~/Scripts/notifier.js",
                "~/Scripts/mask.js",
                "~/Scripts/angularjs-dropdown-multiselect.min.js",
                "~/Scripts/chk-multi-select.js",
                "~/Scripts/angular-recaptcha.js"
                //angularjs-dropdown-multiselect.min.js
                ));

            bundles.Add(new ScriptBundle("~/bundles/fccApp").Include(
                "~/Scripts/app/Scripts/app.js",
                "~/Scripts/app/Scripts/checklist-model.js",
                "~/Scripts/app/Scripts/accessible-form.js",
                "~/Scripts/app/Scripts/app-factories.js",
                "~/Scripts/chat/chat.js",
                "~/Scripts/chat/socketio.js",
                "~/Scripts/app/Scripts/routeConfigurator.js",
                "~/Scripts/app/Scripts/fileUploader.js",
                "~/Scripts/app/Scripts/mapAreaConfigurator.js",
                "~/Scripts/app/Controllers/loginController.js",
                "~/Scripts/app/Controllers/dashBoardController.js",
                "~/Scripts/app/Controllers/usersController.js",
                "~/Scripts/app/Controllers/singleFileConversionController.js",
                "~/Scripts/app/Controllers/singleProfileController.js",
                "~/Scripts/app/Controllers/singleLibraryController.js",
                "~/Scripts/app/Controllers/singleSettingsController.js",
                "~/Scripts/app/Controllers/batchConversionController.js",
                "~/Scripts/app/Controllers/batchProfileController.js",
                "~/Scripts/app/Controllers/batchLibraryController.js",
                "~/Scripts/app/Controllers/batchSettingsController.js",

                  "~/Scripts/app/Controllers/ftpLibraryController.js",
                "~/Scripts/app/Controllers/ftpSettingsController.js",


                  "~/Scripts/app/Controllers/emailLibraryController.js",
                "~/Scripts/app/Controllers/emailSettingsController.js",
                "~/Scripts/app/Controllers/storeController.js",
                "~/Scripts/app/Controllers/userProfileController.js",
                "~/Scripts/app/Controllers/userRestoreController.js",
                "~/Scripts/app/Controllers/userRegistrationController.js"


                )
                );

            bundles.Add(new ScriptBundle("~/bundles/fccServices").Include(
                "~/Scripts/app/Services/usersHttpService.js",
                "~/Scripts/app/Services/manageFilesHttpService.js",
                "~/Scripts/app/Services/documentsHttpService.js",
                "~/Scripts/app/Services/manageUserProfileHttpService.js"
                //manageFilesHttpService
                )
                );
        }
    }
}