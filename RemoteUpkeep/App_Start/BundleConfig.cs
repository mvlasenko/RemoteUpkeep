using System.Web;
using System.Web.Optimization;

namespace RemoteUpkeep
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.unobtrusive-ajax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                //"~/Content/awesome-bootstrap-checkbox.css",
                "~/Content/site.css"));
					  
            //datepicker
            bundles.Add(new StyleBundle("~/Content/datetimepicker").Include(
                "~/Content/bootstrap-datetimepicker.css"));

            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                "~/Scripts/moment-with-locales.js",
                "~/Scripts/bootstrap-datetimepicker.js"));
			
			//file upload
            bundles.Add(new StyleBundle("~/Content/jQuery-File-Upload").Include(
                "~/Content/jQuery.FileUpload/css/jquery.fileupload.css"));

            bundles.Add(new ScriptBundle("~/bundles/jQuery-File-Upload").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jQuery.FileUpload/jquery.iframe-transport.js",
                "~/Scripts/jQuery.FileUpload/jquery.fileupload.js"));

        }
    }
}
