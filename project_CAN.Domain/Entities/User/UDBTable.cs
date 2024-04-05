using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using project_CAN.Domain.Enums;

namespace project_CAN.Domain.Entities.User
{
    [Table("userTable")]
    public class UDBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Username cannot be longer than 50 characters.")]
        public string userName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password cannot be shorter than 8 characters.")]
        public string password { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [StringLength(256)]
        public string email { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public URole privilegies { get; set; }

        public int? tutorialId { get; set; }

        [Required]
        public bool isBlocked { get; set; }

        [Required]
        public DateTime lastLogin { get; set; }
    }
}
