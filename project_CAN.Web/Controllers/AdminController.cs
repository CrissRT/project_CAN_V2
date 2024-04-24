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
    public class AdminController : ModeratorController

    {
    private readonly IAdmin _adminBL;
    
    public AdminController()
    {
        var bl = new BussinesLogic();
        _adminBL = bl.GetAdminBL();
    }

    public ActionResult ControlUsers()
    {
        if (!isUserAdmin())
        {
            return RedirectToAction("Index", "Home");
        }

        ViewBag.users = _adminBL.GetAllUsersExceptAdminFromDB(RetrieveUserID());

        return View();
    }

    public ActionResult ControlContent()
    {
        if (!isUserAdmin())
        {
            return RedirectToAction("Index", "Home");
        }

        ViewBag.path = insideProjectDirectory;
        ViewBag.content = _adminBL.GetAllContentFromDB();

        return View();
    }

    public ActionResult AddContent()
    {
        if (!isUserAdmin())
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddContent(ContentView viewModel)
    {
        if (!isUserAdmin()) return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public ActionResult RemoveContent(int id)
    {
        if (!isUserAdmin())
        {
            return RedirectToAction("Index", "Home");
        }

        _adminBL.RemoveContentFromDB(id, pathImagesContent);

        return RedirectToAction("ControlContent", "Admin");
    }

    public ActionResult EditUser(int id)
    {
        if (!isUserAdmin())
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
        if (!isUserAdmin())
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
        if (!isUserAdmin())
        {
            return RedirectToAction("Index", "Home");
        }

        _adminBL.DeleteUserFromDB(id);
        return RedirectToAction("ControlUsers", "Admin");
    }
    }
}