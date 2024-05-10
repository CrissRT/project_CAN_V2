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
                     "~/Content/UserStyles/Index.css"));

            bundles.Add(new StyleBundle("~/Content/css/user/header").Include(
                "~/Content/UserStyles/Header.css"));

            bundles.Add(new StyleBundle("~/Content/css/user/profile").Include(
                "~/Content/UserStyles/Profile.css"));

            bundles.Add(new StyleBundle("~/Content/css/signup").Include(
                "~/Content/UserStyles/SignUp.css"));

            bundles.Add(new StyleBundle("~/Content/css/login").Include(
                "~/Content/UserStyles/Login.css"));

            bundles.Add(new StyleBundle("~/Content/css/admin/users").Include(
                "~/Content/AdminStyles/ControlUsers.css"));

            bundles.Add(new StyleBundle("~/Content/css/admin/tutorial").Include(
                "~/Content/AdminStyles/ControlTutorial.css"));

            bundles.Add(new StyleBundle("~/Content/css/moderator/tutorial").Include(
                "~/Content/ModeratorStyles/ControlTutorial.css"));

            bundles.Add(new StyleBundle("~/Content/css/admin/editUser").Include(
                "~/Content/AdminStyles/EditUser.css"));

            bundles.Add(new StyleBundle("~/Content/css/admin/editTutorial").Include(
                "~/Content/AdminStyles/EditTutorial.css"));

            bundles.Add(new StyleBundle("~/Content/css/moderator/editTutorial").Include(
                "~/Content/ModeratorStyles/EditTutorial.css"));

            bundles.Add(new StyleBundle("~/Content/css/admin/addTutorial").Include(
                "~/Content/AdminStyles/AddTutorial.css"));

            bundles.Add(new StyleBundle("~/Content/css/moderator/addTutorial").Include(
                "~/Content/ModeratorStyles/AddTutorial.css"));

            bundles.Add(new StyleBundle("~/Content/css/user/editProfile").Include(
                "~/Content/UserStyles/EditProfile.css"));

            bundles.Add(new StyleBundle("~/Content/css/admin/editProfile").Include(
                "~/Content/AdminStyles/EditProfile.css"));

            bundles.Add(new StyleBundle("~/Content/css/moderator/editProfile").Include(
                "~/Content/ModeratorStyles/EditProfile.css"));

            bundles.Add(new StyleBundle("~/Content/css/admin/header").Include(
                "~/Content/AdminStyles/HeaderAdmin.css"));

            bundles.Add(new StyleBundle("~/Content/css/moderator/header").Include(
                "~/Content/ModeratorStyles/HeaderModerator.css"));

            bundles.Add(new StyleBundle("~/Content/css/admin/profile").Include(
                "~/Content/AdminStyles/Profile.css"));

            bundles.Add(new StyleBundle("~/Content/css/moderator/profile").Include(
                "~/Content/ModeratorStyles/Profile.css"));

            bundles.Add(new StyleBundle("~/Content/css/user/likedTutorials").Include(
                "~/Content/UserStyles/LikedTutorials.css"));

            bundles.Add(new StyleBundle("~/Content/css/user/watchTutorial").Include(
                "~/Content/UserStyles/WatchTutorial.css"));

            bundles.Add(new StyleBundle("~/Content/css/user/loggedIn").Include(
                "~/Content/UserStyles/LoggedIn.css"));
        }
    }
}
