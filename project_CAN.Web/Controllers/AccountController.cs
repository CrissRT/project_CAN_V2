﻿using System;
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
using project_CAN.Web.Models.User;

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
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Profile()
        {
            if (!isUserLogged())
            {
                return RedirectToAction("Login", "Account");
            }

            var apiCookie = Request.Cookies["X-KEY"];
            var profile = _user.GetUserByCookie(apiCookie.Value);

            ViewBag.userName = profile.userName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfileView editData)
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EditProfileView, EditProfile>());
            var data = Mapper.Map<EditProfile>(editData);
            var response = _user.EditProfileFromDB(data);
            TempData["message"] = response.StatusMsg;
            return RedirectToAction("EditProfile", "Account");
        }

        public ActionResult EditProfile()
        {
            if (!isUserLogged())
            {
                return RedirectToAction("Login", "Account");
            }

            var profile = _user.GetUserByCookie(Request.Cookies["X-KEY"].Value);

            ViewBag.user = profile;
            return View();
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
                var userRegister = _user.UserRegistrationSessionBL(data);
                if (userRegister.Status)
                {
                    HttpCookie cookie = _user.GenCookie(registrationViewData.email);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "S-a petrecut o eroare!";
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

                var userLogin = _user.UserLoginSessionBL(data);
                if (userLogin.Status)
                {
                    HttpCookie cookie = _user.GenCookie(login.credential);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "S-a petrecut o eroare!";
                }
            }

            return View();
        }
    }
}