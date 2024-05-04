using System.ComponentModel.DataAnnotations;

namespace project_CAN.Web.Models.User
{
    public class UserRegistrationView
    {
        [Required(ErrorMessage = "{0} este necesar!")]
        [Display(Name = "Email-ul")]
        public string email { get; set; }

        [Required(ErrorMessage = "{0} este necesar!")]
        [Display(Name = "Username-ul")]
        public string username { get; set; }

        [Required(ErrorMessage = "{0} este necesară!")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string password { get; set; }

        [Required(ErrorMessage = "{0} este necesară!")]
        [DataType(DataType.Password)]
        [Display(Name = "Repeta parola")]
        public string repeatPassword { get; set; }
    }
}