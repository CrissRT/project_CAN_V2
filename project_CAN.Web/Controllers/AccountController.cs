using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project_CAN.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Profile()
        {
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

        public ActionResult test()
        {
            return View();
        }
    }
}