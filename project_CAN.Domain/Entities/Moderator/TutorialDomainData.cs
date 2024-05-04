using System.Web;

namespace project_CAN.Domain.Entities.Moderator
{
    public class TutorialDomainData
    {
        public string title { get; set; }
        public string description { get; set; }
        public string videoLink { get; set; }
        public HttpPostedFileBase image { get; set; }

        public int tutorialId { get; set; }
    }
}
