using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_CAN.Domain.Enums;

namespace project_CAN.Domain.Entities.User
{
    public class UserMinimal
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public DateTime lastLogin { get; set; }
        public URole privilegies { get; set; }
    }
}
