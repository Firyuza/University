﻿using System.Web;
using System.Web.Optimization;

namespace WebUniversity
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

            bundles.Add(new ScriptBundle("~/bundles/report").Include(
                "~/Scripts/data/report-data.js"));

            bundles.Add(new ScriptBundle("~/bundles/academic").Include(
                "~/Scripts/data/academic-data.js"));

            bundles.Add(new ScriptBundle("~/bundles/schedule").Include(
                "~/Scripts/data/schedule-data.js"));

            bundles.Add(new ScriptBundle("~/bundles/underscore").Include(
                        "~/Scripts/underscore-min.js"));
        }
    }
}
