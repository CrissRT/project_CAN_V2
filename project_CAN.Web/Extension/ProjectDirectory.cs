using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using project_CAN.Domain.Entities.User;

namespace project_CAN.Web.Extension
{
    public class ProjectDirectory
    {
        public static readonly string insideProjectDirectory = "~/Content/ImagesTutorial";
        public static readonly string pathImagesTutorial = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content", "ImagesTutorial");
    }
}