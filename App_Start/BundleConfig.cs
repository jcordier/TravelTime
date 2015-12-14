using System.Web;
using System.Web.Optimization;

namespace TravelTime
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/lib/underscore.js",
                        "~/Scripts/lib/angular/angular.js",
                        "~/Scripts/lib/angular-google-maps/angular-google-maps.js"));

            bundles.Add(new ScriptBundle("~/bundles/cordovaand").Include(
                        "~/cordova/cordova.js",
                        "~/cordova/cordova-plugins.js"));

            bundles.Add(new ScriptBundle("~/bundles/location").Include(
                        "~/Scripts/location/location.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
