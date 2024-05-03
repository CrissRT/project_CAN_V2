using project_CAN.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_CAN.BusinessLogic;
using project_CAN.Domain.Enums;
using project_CAN.Web.Extension;

namespace project_CAN.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly string insideProjectDirectory = ProjectDirectory.insideProjectDirectory;
        // GET: Home
        public ActionResult Index()
        {
            int userLogged = isUserLogged();
            if (userLogged == 2) return RedirectToAction("ControlUsers", "Admin");
            else if (userLogged == 0) return RedirectToAction("LoggedIn", "Home");
            
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (isUserLogged() == -1) return RedirectToAction("Login", "Account");
            else if (isUserLogged() == 2) return RedirectToAction("ControlUsers", "Admin");

            ViewBag.path = insideProjectDirectory;
            ViewBag.tutorial = _user.GetAllTutorialFromDB();

            return View();
        }
    }
}