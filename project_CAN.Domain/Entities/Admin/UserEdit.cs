using project_CAN.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_CAN.Domain.Entities.Admin
{
    public class UserEdit
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public URole privilegies { get; set; }
        public bool isBlocked { get; set; }
    }
}