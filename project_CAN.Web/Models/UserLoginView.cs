using System;
using System.ComponentModel.DataAnnotations;

namespace project_CAN.Web.Models
{
    public class UserLoginView
    {
        [Required]
        [Display(Name = "Email")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastLogin { get; set; }

    }
}