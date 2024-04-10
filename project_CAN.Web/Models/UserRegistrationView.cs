using System;
using System.ComponentModel.DataAnnotations;

namespace project_CAN.Web.Models
{
    public class UserRegistrationView
    {
        [Required]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat Password")]
        public string repeatPassword { get; set; }
    }
}