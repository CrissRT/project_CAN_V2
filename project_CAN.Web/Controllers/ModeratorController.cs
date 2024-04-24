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

        protected readonly string insideProjectDirectory = "~/Content/ImagesContent";

        protected readonly string pathImagesContent =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content", "ImagesContent");
        // GET: Moderator
        public ActionResult Profile()
        {
            return View();
        }
    }
}