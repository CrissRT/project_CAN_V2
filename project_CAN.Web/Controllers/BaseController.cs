using project_CAN.BusinessLogic;
using project_CAN.BusinessLogic.Interfaces;
using project_CAN.Web.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_CAN.Domain.Entities.User;
using project_CAN.Domain.Enums;

namespace project_CAN.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUser _user;

        public BaseController()
        {
            var bl = new BussinesLogic();
            _user = bl.GetSessionBL();
        }

        protected short isUserLogged()
        {
            var profile = userLogged();
            if (profile != null)
            {
                if (profile.privilegies == URole.user) return 0;
                else if (profile.privilegies == URole.moderator) return 1;
                else if (profile.privilegies == URole.admin) return 2;
            }

            return -1;
        }
        
        private UserMinimal userLogged()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return null;
            }
            var apiCookie = Request.Cookies["X-KEY"];

            if (apiCookie != null)
            {
                var profile = _user.GetUserByCookie(apiCookie.Value);

                if (profile != null)
                {
                    return profile;
                }
            }
            return null;
        }


        protected int RetrieveUserID()
        {
            var apiCookie = Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _user.GetUserByCookie(apiCookie.Value);
                if (profile != null)
                {
                    return profile.userId;
                }
            }
            return -1;
        }

        internal void SessionStatus()
        {
            var apiCookie = Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _user.GetUserByCookie(apiCookie.Value);
                if (profile != null)
                {
                    System.Web.HttpContext.Current.SetMySessionObject(profile);
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "login";
                }
                else
                {
                    System.Web.HttpContext.Current.Session.Clear();
                    if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("X-KEY"))
                    {
                        var cookie = ControllerContext.HttpContext.Request.Cookies["X-KEY"];
                        if (cookie != null)
                        {
                            cookie.Expires = DateTime.Now.AddDays(-1);
                            ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                        }
                    }

                    System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
                }
            }
            else
            {
                System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
            }
        }
    }
}