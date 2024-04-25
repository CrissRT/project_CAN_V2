using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_CAN.Web.Models.Admin
{
    public class ContentView
    {
        public int tutorialId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string videoLink { get; set; }
        public HttpPostedFileBase image { get; set; }
    }
}