using System.Web;
using System.Web.Optimization;

namespace BudgetApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Site Template/js/appear.js",
                        "~/Site Template/js/custom.js",
                        "~/Site Template/js/gmap3.min.js",
                        "~/Site Template/js/jquery.fitvids.js",
                        "~/Site Template/js/jquery.magnific-popup.min.js",
                        "~/Site Template/js/jquery.mb.YTPlayer.min.js",
                        "~/Site Template/js/jquery.parallax-1.1.3.js",
                        "~/Site Template/js/jquery.simple-text-rotator.min.js",
                        "~/Site Template/js/jquery.superslides.min.js",
                        "~/Site Template/js/owl.carousel.min.js",
                        "~/Site Template/js/smoothscroll.js",
                        "~/Site Template/js/submenu-fix.js",
                        "~/Site Template/js/wow.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Site Template/css/bootstrap-theme.css",
                      "~/Site Template/css/greensea.css",
                      "~/Site Template/css/magnific-popup.css",
                      "~/Site Template/css/owl.carousel.css",
                      "~/Site Template/css/simpletextrotator.css",
                      "~/Site Template/css/stroke-gap-icons.css",
                      "~/Site Template/css/superslides.css",
                      "~/Site Template/css/vertical.min.css",
                      "~/Site Template/css/style.css",
                      "~/Content/bootstrap-social.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"
                      ));
        }
    }
}
