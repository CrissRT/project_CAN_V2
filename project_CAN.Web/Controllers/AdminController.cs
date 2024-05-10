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
using project_CAN.Domain.Entities.Moderator;
using project_CAN.Web.Extension;
using project_CAN.Web.Models.Moderator;

namespace project_CAN.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly string insideProjectDirectory = ProjectDirectory.insideProjectDirectory;
        private readonly string pathImagesTutorial = ProjectDirectory.pathImagesTutorial;
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

        public ActionResult ControlTutorial()
        {
            if (isUserLogged() != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.path = insideProjectDirectory;
            ViewBag.tutorial = _adminBL.GetAllTutorialFromDB();

            return View();
        }

        public ActionResult AddTutorial()
        {
            if (isUserLogged() != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTutorial(TutorialView viewModel)
        {
            if (isUserLogged() != 2) return RedirectToAction("Index", "Home");
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<TutorialView, TutorialDomainData>());
            var data = Mapper.Map<TutorialDomainData>(viewModel);
            var addedContent = _adminBL.AddTutorialInDB(data, pathImagesTutorial);
            if (addedContent.Status)
            {
                return RedirectToAction("ControlTutorial", "Admin");
            }

            return View();
        }

        public ActionResult EditTutorial(int id)
        {
            if (isUserLogged() != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.path = insideProjectDirectory;
            ViewBag.tutorial = _adminBL.GetTutorialByIdFromDB(id);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTutorial(TutorialView tutorial)
        {
            if (isUserLogged() != 2) return RedirectToAction("Index", "Home");
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<TutorialView, TutorialDomainData>());
            var data = Mapper.Map<TutorialDomainData>(tutorial);
            var response = _adminBL.EditTutorialInDB(data, pathImagesTutorial);
            if (response.Status)
            {
                return RedirectToAction("ControlTutorial", "Admin");
            }

            return RedirectToAction("EditTutorial", "Admin");
        }

        public ActionResult RemoveContent(int id)
        {
            if (isUserLogged() != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            _adminBL.RemoveTutorialFromDB(id, pathImagesTutorial);

            return RedirectToAction("ControlTutorial", "Admin");
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
            TempData["message"] = response.StatusMsg;
            return RedirectToAction("EditUser", "Admin", new {id = data.userId});
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