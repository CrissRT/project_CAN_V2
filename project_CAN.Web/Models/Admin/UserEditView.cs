using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using project_CAN.Domain.Enums;

namespace project_CAN.Web.Models.Admin
{
    public class UserEditView
    {
        [Required]
        public int userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public URole privilegies { get; set; }
        public bool isBlocked { get; set; }
    }
}