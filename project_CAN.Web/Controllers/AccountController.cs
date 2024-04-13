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
    public class AccountController : BaseController
    {
        public ActionResult Logout()
        {
            // Invalidate the authentication cookie
            var cookie = Request.Cookies["X-KEY"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            // Redirect to the login page or any other page after logout
            return RedirectToAction("Index", "Main");
        }
        public ActionResult Profile()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Account");
            }

            var apiCookie = Request.Cookies["X-KEY"];
            var profile = _session.GetUserByCookie(apiCookie.Value);

            ViewBag.userName = profile.userName;
            return View(ViewBag);
        }
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserRegistrationView registrationViewData)
        {
            if (ModelState.IsValid)
            {
                Mapper.Reset();
                Mapper.Initialize(cfg => cfg.CreateMap<UserRegistrationView, URegistrationData>());
                var data = Mapper.Map<URegistrationData>(registrationViewData);

                //data.lastLogin = DateTime.Now;
                var userRegister = _session.UserRegistrationSessionBL(data);
                if (userRegister.Status)
                {
                    HttpCookie cookie = _session.GenCookie(registrationViewData.email);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    ModelState.AddModelError("", userRegister.StatusMsg);
                }
            }

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

                var userLogin = _session.UserLoginSessionBL(data);
                if (userLogin.Status)
                {
                    HttpCookie cookie = _session.GenCookie(login.credential);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    
                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    ViewBag.Error = userLogin.StatusMsg;
                }
            }

            return View();
        }
    }
}