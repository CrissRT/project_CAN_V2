using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project_CAN.Web.Controllers
{
    public class ModeratorController : BaseController
    {

        protected readonly string insideProjectDirectory = "~/Content/ImagesTutorial";

        protected readonly string pathImagesTutorial =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content", "ImagesTutorial");
        // GET: Moderator
        //public ActionResult Profile()
        //{
        //    return View();
        //}
    }
}