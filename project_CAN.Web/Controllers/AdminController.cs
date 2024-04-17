using project_CAN.BusinessLogic.Interfaces;
using project_CAN.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_CAN.BusinessLogic;
using project_CAN.Web.Models.Admin;
using AutoMapper;
using project_CAN.Domain.Entities.User;
using System.Web.UI.WebControls;
using project_CAN.Domain.Entities.Admin;

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

        public ActionResult ControlContent()
        {
            if (!isUserAdmin())
            {
                return RedirectToAction("Index", "Main");
            }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserEditView editData)
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<UserEditView, UserEdit>());
            var data = Mapper.Map<UserEdit>(editData);
            if (!isUserAdmin())
            {
                return RedirectToAction("Index", "Main");
            }

            var response = _adminBL.EditUserInDB(data);
            if (response.Status)
            {
                return RedirectToAction("ControlUsers", "Admin");
            }

            return RedirectToAction("EditUser", "Admin");
        }

        public ActionResult EditContent()
        {
            if (!isUserAdmin())
            {
                return RedirectToAction("Index", "Main");
            }
            //ViewBag.user = _adminBL.GetUserByIdFromDB(id);
            return View();
        }

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