using project_CAN.BusinessLogic.Interfaces;
using project_CAN.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_CAN.BusinessLogic;

namespace project_CAN.Web.Controllers
{
    public class AdminController : BaseController
    { 
        public ActionResult ControlUsers()
        {
            var apiCookie = Request.Cookies["X-KEY"];

            if (apiCookie != null)
            {
                var profile = _session.GetUserByCookie(apiCookie.Value);

                if (profile != null && profile.privilegies != URole.admin)
                {
                    return RedirectToAction("Index", "Main");
                }
            }

            return View();
        }

        public ActionResult ControlContent()
        {
            var apiCookie = Request.Cookies["X-KEY"];

            if (apiCookie != null)
            {
                var profile = _session.GetUserByCookie(apiCookie.Value);

                if (profile != null && profile.privilegies != URole.admin)
                {
                    return RedirectToAction("Index", "Main");
                }
            }

            return View();
        }
    }
}