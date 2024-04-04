using System;
using System.ComponentModel.DataAnnotations;

namespace project_CAN.Web.Models
{
    public class UserLoginView
    {
        [Required]
        [Display(Name = "Email or Username")]
        public string credential { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime lastLogin { get; set; }

    }
}