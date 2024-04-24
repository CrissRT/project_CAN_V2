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
        protected readonly string insideProjectDirectory = "~/Content/ImagesContent";

        protected readonly string pathImagesContent =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content", "ImagesContent");
        private readonly IAdmin _adminBL;
        
        public AdminController()
        {
            var bl = new BussinesLogic();
            _adminBL = bl.GetAdminBL();
        }


        public ActionResult Profile()
        {
            var userLogged = isUserLogged();
            if (userLogged == -1) return RedirectToAction("Login", "Account");
            else if (userLogged != 2) return RedirectToAction("Index", "Home");

            var profile = _user.GetUserByCookie(Request.Cookies["X-KEY"].Value);

            ViewBag.userName = profile.userName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfileView editData)
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EditProfileView, EditProfile>());
            var data = Mapper.Map<EditProfile>(editData);
            var response = _user.EditProfileFromDB(data);
            TempData["message"] = response.StatusMsg;
            return RedirectToAction("EditProfile", "Admin");
        }

        public ActionResult EditProfile()
        {
            var userLogged = isUserLogged();
            if (userLogged == -1) return RedirectToAction("Login", "Account");
            else if (userLogged != 2) return RedirectToAction("Index", "Home");

            ViewBag.user = _user.GetUserByCookie(Request.Cookies["X-KEY"].Value);
            return View();
        }


        public ActionResult ControlUsers()
        {
            if (isUserLogged() != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.users = _adminBL.GetAllUsersExceptAdminFromDB(RetrieveUserID());

            return View();
        }

        public ActionResult ControlContent()
        {
            if (isUserLogged() != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.path = insideProjectDirectory;
            ViewBag.content = _adminBL.GetAllContentFromDB();

            return View();
        }

        public ActionResult AddContent()
        {
            if (isUserLogged() != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddContent(ContentView viewModel)
        {
            if (isUserLogged() != 2) return RedirectToAction("Index", "Home");
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
            if (isUserLogged() != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult EditContent(ContentView content)
        {

            return RedirectToAction("EditContent", "Admin");
        }

        public ActionResult RemoveContent(int id)
        {
            if (isUserLogged() != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            _adminBL.RemoveContentFromDB(id, pathImagesContent);

            return RedirectToAction("ControlContent", "Admin");
        }

        public ActionResult EditUser(int id)
        {
            if (isUserLogged() != 2)
            {
                return RedirectToAction("Index", "Home");
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
            if (isUserLogged() != 2)
            {
                return RedirectToAction("Index", "Home");
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
            if (isUserLogged() != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            _adminBL.DeleteUserFromDB(id);
            return RedirectToAction("ControlUsers", "Admin");
        }
    }
}