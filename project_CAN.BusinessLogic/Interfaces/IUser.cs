using System.Web;
using project_CAN.Domain.Entities.User;

namespace project_CAN.BusinessLogic.Interfaces
{
    public interface IUser
    {
        UResponse UserLoginSessionBL(ULoginData data);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string apiCookieValue);
        UResponse UserRegistrationSessionBL(URegistrationData data);

        UResponse EditProfileFromDB(EditProfile data);
    }
}