using AutoMapper;
using project_CAN.BusinessLogic;
using project_CAN.Domain.Entities.Moderator;
using project_CAN.Domain.Entities.User;
using project_CAN.Web.Models.Moderator;
using project_CAN.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_CAN.BusinessLogic.Interfaces;
using project_CAN.Web.Extension;

namespace project_CAN.Web.Controllers
{
    public class ModeratorController : BaseController
    {

        private readonly string insideProjectDirectory = ProjectDirectory.insideProjectDirectory;
        private readonly string pathImagesTutorial = ProjectDirectory.pathImagesTutorial;
        private readonly IModerator _moderatorBL;

        public ModeratorController()
        {
            var bl = new BussinesLogic();
            _moderatorBL = bl.GetModeratorBL();
        }

        public ActionResult Profile()
        {
            var userLogged = isUserLogged();
            if (userLogged == -1) return RedirectToAction("Login", "Account");
            else if (userLogged != 1) return RedirectToAction("Index", "Home");

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
            return RedirectToAction("EditProfile", "Moderator");
        }

        public ActionResult EditProfile()
        {
            var userLogged = isUserLogged();
            if (userLogged == -1) return RedirectToAction("Login", "Account");
            else if (userLogged != 1) return RedirectToAction("Index", "Home");

            ViewBag.user = _user.GetUserByCookie(Request.Cookies["X-KEY"].Value);
            return View();
        }
        
        public ActionResult ControlTutorial()
        {
            if (isUserLogged() != 1)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.path = insideProjectDirectory;
            ViewBag.tutorial = _moderatorBL.GetAllTutorialFromDB();

            return View();
        }

        public ActionResult AddTutorial()
        {
            if (isUserLogged() != 1)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTutorial(TutorialView viewModel)
        {
            if (isUserLogged() != 1) return RedirectToAction("Index", "Home");
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<TutorialView, TutorialDomainData>());
            var data = Mapper.Map<TutorialDomainData>(viewModel);
            var addedContent = _moderatorBL.AddTutorialInDB(data, pathImagesTutorial);
            if (addedContent.Status)
            {
                return RedirectToAction("ControlTutorial", "Moderator");
            }

            return View();
        }

        public ActionResult EditTutorial(int id)
        {
            if (isUserLogged() != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.path = insideProjectDirectory;
            ViewBag.tutorial = _moderatorBL.GetTutorialByIdFromDB(id);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTutorial(TutorialView tutorial)
        {
            if (isUserLogged() != 1) return RedirectToAction("Index", "Home");
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<TutorialView, TutorialDomainData>());
            var data = Mapper.Map<TutorialDomainData>(tutorial);
            var response = _moderatorBL.EditTutorialInDB(data, pathImagesTutorial);
            if (response.Status)
            {
                return RedirectToAction("ControlTutorial", "Moderator");
            }

            return RedirectToAction("EditTutorial", "Moderator");
        }

        public ActionResult RemoveContent(int id)
        {
            if (isUserLogged() != 1)
            {
                return RedirectToAction("Index", "Home");
            }

            _moderatorBL.RemoveTutorialFromDB(id, pathImagesTutorial);

            return RedirectToAction("ControlTutorial", "Moderator");
        }
    }
}