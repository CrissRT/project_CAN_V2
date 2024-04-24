using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_CAN.Domain.Entities.User
{
    public class EditProfile
    { 
        public string apiCookie { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

        public string email { get; set; }
    }
}
