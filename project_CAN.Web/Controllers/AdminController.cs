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
            if (!isUserAdmin() )
            {
                return RedirectToAction("Index", "Main");
            }
            
            return View();
        }

        public ActionResult ControlContent()
        {
            if (!isUserAdmin())
            {
                return RedirectToAction("Index", "Main");
            }

            return View();
        }
    }
}