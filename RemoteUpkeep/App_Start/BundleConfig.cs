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
                "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/flickr.js",
                "~/Scripts/flexslider.min.js",
                "~/Scripts/lightbox.min.js",
                "~/Scripts/masonry.min.js",
                "~/Scripts/twitterfetcher.min.js",
                "~/Scripts/spectragram.min.js",
                "~/Scripts/ytplayer.min.js",
                "~/Scripts/countdown.min.js",
                "~/Scripts/smooth-scroll.min.js",
                "~/Scripts/parallax.js",
                "~/Scripts/scripts.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/themify-icons.css",
                "~/Content/flexslider.css",
                "~/Content/lightbox.min.css",
                "~/Content/ytplayer.css",
                "~/Content/theme.css",
                "~/Content/custom.css",
                "~/Content/Site.css"
                ));
					  
            //datepicker
            bundles.Add(new StyleBundle("~/Content/datetimepicker").Include(
                "~/Content/bootstrap-datetimepicker.css"));

            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                "~/Scripts/moment-with-locales.js",
                "~/Scripts/bootstrap-datetimepicker.js"));
			
			//file upload
            bundles.Add(new StyleBundle("~/Content/fileupload").Include(
                "~/Content/jQuery.FileUpload/css/jquery.fileupload.css"));

            bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jQuery.FileUpload/jquery.iframe-transport.js",
                "~/Scripts/jQuery.FileUpload/jquery.fileupload.js",
                "~/Scripts/fileupload.js"));

            //admin area scripts
            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                "~/Scripts/admin.js"));
            
            //geography scripts
            bundles.Add(new ScriptBundle("~/bundles/geography").Include(
                "~/Scripts/geography.js"));

            //dropzone
            bundles.Add(new StyleBundle("~/Content/dropzone").Include(
                "~/Scripts/dropzone/dropzone.css"));

            bundles.Add(new ScriptBundle("~/bundles/dropzone").Include(
                "~/Scripts/dropzone/dropzone.js",
                "~/Scripts/dropzone.js"));

        }
    }
}