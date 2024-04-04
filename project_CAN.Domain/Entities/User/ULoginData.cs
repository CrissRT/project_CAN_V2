using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_CAN.Domain.Enums;
namespace project_CAN.Domain.Entities.User
{
    public class ULoginData
    {
        public string password { get; set; }
        public string email { get; set; }
        public URole privilegies { get; set; }


    }
}
