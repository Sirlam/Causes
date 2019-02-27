using System.Web;
using System.Web.Optimization;

namespace Causes.UI.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/swiper").Include(
                      "~/Content/swiper.css"));

            bundles.Add(new ScriptBundle("~/bundles/swiperjs").Include(
                      "~/Scripts/swiper.js"));

            bundles.Add(new StyleBundle("~/Content/fancybox").Include(
                      "~/Content/jquery.fancybox.css"));

            bundles.Add(new ScriptBundle("~/bundles/fancyjs").Include(
                      "~/Scripts/jquery.fancybox.js"));

            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                      "~/Content/all.css"));

            bundles.Add(new StyleBundle("~/Content/datatable").Include(
                      "~/Content/datatables.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatablejs").Include(
                      "~/Scripts/datatables.js"));
        }
    }
}
