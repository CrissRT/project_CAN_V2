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

            ViewBag.users = _adminBL.GetAllUsersExceptAdminFromDB(RetrieveUserID());
            
            return View();
        }
        public ActionResult EditUser(int id)
        {
            if (!isUserAdmin())
            {
                return RedirectToAction("Index", "Main");
            }
            ViewBag.user = _adminBL.GetUserByIdFromDB(id);
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

        //public ActionResult EditUser()
        //{
        //    if (!isUserAdmin())
        //    {
        //        return RedirectToAction("Index", "Main");
        //    }
        //    return View();
        //}

        //public ActionResult AddUser()
        //{
        //    if (!isUserAdmin())
        //    {
        //        return RedirectToAction("Index", "Main");
        //    }
        //    _adminBL.AddUser(id);
        //    return View();
        //}

        public ActionResult DeleteUser(int id)
        {
            if (!isUserAdmin())
            {
                return RedirectToAction("Index", "Main");
            }

            _adminBL.DeleteUserFromDB(id);
            return RedirectToAction("ControlUsers", "Admin");
        }
    }
}