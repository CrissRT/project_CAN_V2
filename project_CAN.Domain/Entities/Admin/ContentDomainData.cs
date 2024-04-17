using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace project_CAN.Domain.Entities.Admin
{
    public class ContentDomainData
    {
        public string title { get; set; }
        public string description { get; set; }
        public string videoLink { get; set; }
        public HttpPostedFileBase image { get; set; }
    }
}
