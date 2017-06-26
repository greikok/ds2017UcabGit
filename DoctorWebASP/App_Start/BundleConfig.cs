using System.Web;
using System.Web.Optimization;

namespace DoctorWebASP
{
    public class BundleConfig
    {
        // Author: Carlos Valls
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/plugins/jQuery/jquery-2.2.3.min.js",
                      "~/Content/bootstrap/js/bootstrap.min.js",
                      "~/Scripts/jquery-ui.min.js",
                      "~/Scripts/moment.min.js",
                      "~/Content/plugins/daterangepicker/daterangepicker.js",
                      "~/Content/plugins/slimScroll/jquery.slimscroll.min.js",
                      "~/Content/plugins/fastclick/fastclick.js",                      
                      "~/Content/dist/js/app.min.js",
                      "~/Content/dist/js/demo.js",
                      "~/Scripts/respond.js",
                      "~/Content/plugins/fullcalendar/fullcalendar.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/css/bootstrap.min.css",
                      "~/Content/dist/css/skins/skin-red-light.css",
                      "~/Content/plugins/daterangepicker/daterangepicker.css",
                      "~/Content/dist/css/AdminLTE.min.css",
                      "~/Content/plugins/fullcalendar/fullcalendar.min.css",
                      "~/Content/dist/css/skins/_all-skins.min.css"));
            BundleTable.EnableOptimizations = true; 
        }
    }
}
