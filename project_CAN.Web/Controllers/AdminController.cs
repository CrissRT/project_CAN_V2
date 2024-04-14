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
        private readonly IAdmin _adminBL;

        public AdminController()
        {
            var bl = new BussinesLogic();
            _adminBL = bl.GetAdminBL();
        }
        public ActionResult ControlUsers()
        {
            if (!isUserAdmin() )
            {
                return RedirectToAction("Index", "Main");
            }

            ViewBag.users = _adminBL.GetAllUsersFromDB();
            
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