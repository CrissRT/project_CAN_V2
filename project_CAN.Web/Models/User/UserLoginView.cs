using System.ComponentModel.DataAnnotations;

namespace project_CAN.Web.Models.User
{
    public class UserLoginView
    {
        [Required(ErrorMessage = "{0} este necesar!")]
        [Display(Name = "Email sau username")]
        public string credential { get; set; }

        [Required(ErrorMessage = "{0} este necesară!")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string password { get; set; }
    }
}