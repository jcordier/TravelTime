﻿using System.Web;
using System.Web.Optimization;

namespace TravelTime
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/lib/underscore.js",
                        "~/Scripts/lib/angular/angular.js",
                        "~/Scripts/lib/angular-google-maps/angular-google-maps.js"));

            bundles.Add(new ScriptBundle("~/bundles/cordova").Include(
                        "~/cordova/cordova.js",
                        "~/cordova/js/index.js",
                        "~/cordova/cordova-plugins.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/location").Include(
                        "~/Scripts/location/location.js"));

            bundles.Add(new ScriptBundle("~/bundles/battery").Include(
            "~/Scripts/battery/battery.js"));

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
