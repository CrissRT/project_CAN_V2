using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using project_CAN.BusinessLogic;
using project_CAN.BusinessLogic.Interfaces;

using project_CAN.Domain.Entities.User;
using project_CAN.Web.Extension;


//using project_CAN.Web.Extension;
using project_CAN.Web.Models;

namespace project_CAN.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISession _session;

        public AccountController()
        {
            var bl = new BussinesLogic();
            _session = bl.GetSessionBL();
        }
        // GET: Account
        public ActionResult Profile()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginView login)
        {
            if (ModelState.IsValid)
            {
                Mapper.Reset();
                Mapper.Initialize(cfg => cfg.CreateMap<UserLoginView, ULoginData>());
                var data = Mapper.Map<ULoginData>(login);

                data.lastLogin = DateTime.Now;

                var userLogin = _session.UserLoginView(data);
                if (userLogin.Status)
                {
                    HttpCookie cookie = _session.GenCookie(login.credential);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    ModelState.AddModelError("", userLogin.StatusMsg);
                    return View();
                }
            }

            return View();
        }

        public void SessionStatus()
        {
            var apiCookie = Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _session.GetUserByCookie(apiCookie.Value);
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