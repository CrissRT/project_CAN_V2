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
using project_CAN.Web.Models;
using System.IO;

namespace project_CAN.Web.Controllers
{
    public class AdminController : BaseController
    { 
        private readonly IAdmin _adminBL;
        private readonly string insideProjectDirectory = "~/Content/ImagesContent";
        private readonly string pathImagesContent = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content", "ImagesContent");


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

            ViewBag.path = insideProjectDirectory;
            ViewBag.content = _adminBL.GetAllContentFromDB();

            return View();
        }

        public ActionResult AddContent()
        {
            if (!isUserAdmin())
            {
                return RedirectToAction("Index", "Main");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddContent(ContentView viewModel)
        {
            if (!isUserAdmin()) return RedirectToAction("Index", "Main");
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<ContentView, ContentDomainData>());
            var data = Mapper.Map<ContentDomainData>(viewModel);
            var addedContent = _adminBL.AddContentInDB(data, pathImagesContent);
            if (addedContent.Status)
            {
                return RedirectToAction("ControlContent", "Admin");
            }
            return View();
        }

        public ActionResult EditContent()
        {
            if (!isUserAdmin())
            {
                return RedirectToAction("Index", "Main");
            }
            return View();
        }

        public ActionResult RemoveContent()
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