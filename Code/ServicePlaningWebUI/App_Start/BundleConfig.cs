using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ServicePlaningWebUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js", "~/Scripts/jquery-ui-{version}.js", "~/Scripts/jquery-mask.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker").Include("~/Scripts/bootstrap-datepicker.js", "~/Scripts/locales/bootstrap-datepicker.ru.js"));
            //bundles.Add(new ScriptBundle("~/bundles/bootstrap-validate").Include("~/Scripts/bootstrapValidator.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery-validate").Include("~/Scripts/jquery.validate.js", "~/Scripts/jquery.validate.unobtrusive.js"));//, "~/Scripts/jquery.validate.unobtrusive.bootstrap.js", "~/Scripts/bootstrap.validate.js", "~/Scripts/validate.js"

            bundles.Add(new ScriptBundle("~/bundles/jquery-filterByText").Include("~/Scripts/jquery.filterByText.js"));

            bundles.Add(new ScriptBundle("~/bundles/arr").Include("~/Scripts/arr.js"));

            bundles.Add(new ScriptBundle("~/bundles/sessionstorage").Include("~/Scripts/sessionstorage.{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/tristate").Include("~/Scripts/tristate-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/selmem").Include("~/Scripts/selmem.js"));

            bundles.Add(new ScriptBundle("~/bundles/filter-exp-mem").Include("~/Scripts/filter-exp-mem.js"));

            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include("~/Scripts/fullcalendar.js"));

            bundles.Add(new ScriptBundle("~/bundles/commonjs").Include("~/Scripts/Site.js"));
            bundles.Add(new ScriptBundle("~/bundles/log4js").Include("~/Scripts/log4javascript.js"));
            BundleTable.EnableOptimizations = true;
        }
    }
}