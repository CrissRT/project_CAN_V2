using project_CAN.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_CAN.BusinessLogic;
using project_CAN.Domain.Enums;

namespace project_CAN.Web.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            var apiCookie = Request.Cookies["X-KEY"];

            if (apiCookie != null)
            {
                var profile = _session.GetUserByCookie(apiCookie.Value);

                if (profile != null && profile.privilegies == URole.admin)
                {
                    return RedirectToAction("ControlUsers", "Admin");
                }
            }

            return View();
        }
    }
}