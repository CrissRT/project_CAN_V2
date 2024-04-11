using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_CAN.Domain.Enums;
namespace project_CAN.Domain.Entities.User
{
    public class URegistrationData
    {
        public string password { get; set; }
        public string repeatPassword { get; set; }
        public string email { get; set; }

        public string username { get; set; }
        public DateTime lastLogin { get; set; }
    }
}