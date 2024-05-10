using System.Web;

namespace project_CAN.Web.Models.Moderator
{
    public class TutorialView
    {
        public int tutorialId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string videoLink { get; set; }
        public HttpPostedFileBase image { get; set; }
    }
}