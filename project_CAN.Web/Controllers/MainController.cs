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
            if (isUserAdmin())
            {
                return RedirectToAction("ControlUsers", "Admin");
            }

            return View();
        }
    }
}