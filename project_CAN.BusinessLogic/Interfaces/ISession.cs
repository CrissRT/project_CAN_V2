using System.Web;
using project_CAN.Domain.Entities.User;

namespace project_CAN.BusinessLogic.Interfaces
{
    public interface ISession
    {
        UResponseLogin UserLoginSessionBL(ULoginData data);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string apiCookieValue);

        UResponseLogin UserRegistrationSessionBL(URegistrationData  data);
    }
}