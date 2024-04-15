using System.Data.Entity.Infrastructure;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.UI.WebControls;

namespace project_CAN
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Script Bundle Example
            // bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            // Bootstrap Bundle Example
            // bundles.Add(new Bundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));

            // CSS Bundle Example
            // bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jQuery.3.7.1/Content/Scripts/jquery-{version}.js"));


            // Bootstrap Bundles
            bundles.Add(new Bundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap/dist/js/bootstrap.min.js"));

            // CSS Bundles
            bundles.Add(new StyleBundle("~/Content/css/bootstrap").Include(
                     "~/Content/index.css", "~/Content/bootstrap.min.css",
                     "~/Content/bootstrap-icons-1.11.3/font/bootstrap-icons.css"));

            bundles.Add(new StyleBundle("~/Content/css/mainPage").Include(
                     "~/Content/UserStyles/Index.css", "~/Content/UserStyles/Header.css"));

            bundles.Add(new StyleBundle("~/Content/css/profile").Include(
                "~/Content/UserStyles/Profile.css"));

            bundles.Add(new StyleBundle("~/Content/css/signup").Include(
                "~/Content/UserStyles/SignUp.css"));

            bundles.Add(new StyleBundle("~/Content/css/login").Include(
                "~/Content/UserStyles/Login.css"));

            bundles.Add(new StyleBundle("~/Content/css/admin/users").Include(
                "~/Content/AdminStyles/ControlUsers.css"));

            bundles.Add(new StyleBundle("~/Content/css/admin/content").Include(
                "~/Content/AdminStyles/ControlContent.css"));

            bundles.Add(new StyleBundle("~/Content/css/admin/editUser").Include(
                "~/Content/AdminStyles/EditUser.css"));
        }
    }
}
