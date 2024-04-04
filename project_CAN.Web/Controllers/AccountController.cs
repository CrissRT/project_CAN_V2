using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using project_CAN.Web.Extension;
using project_CAN.Web.Models;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin data)
        {
            if (data == null && ModelState.IsValid) 
            {
                return RedirectToAction("Index", "Main");
            }
            
        }
    }
}