using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using project_CAN.Domain.Enums;

namespace project_CAN.Domain.Entities.User
{
    public class UDBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userID { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Username cannot be longer than 30 characters.")]
        public string userName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password cannot be shorter than 8 characters.")]
        public string password { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [StringLength(30)]
        public string email { get; set; }

        public URole privilegies { get; set; }

        public bool isBlocked { get; set; }
    }
}