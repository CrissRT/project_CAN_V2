using System.Web;
using project_CAN.Domain.Entities.Moderator;
using project_CAN.Domain.Entities.User;

namespace project_CAN.BusinessLogic.Interfaces
{
    public interface IUser
    {
        TutorialsAllData GetAllTutorialFromDB();
        UResponse UserLoginSessionBL(ULoginData data);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string apiCookieValue);
        UResponse UserRegistrationSessionBL(URegistrationData data);

        UResponse EditProfileFromDB(EditProfile data);

        bool LikeAndDislikeinDB(LikesData data);

        int CountAllUserLikesFromDb(int userId);

        LikesAllData GetLikedTutorialsFromDB(int userId);

        DBTutorialTable GetTutorialByIdFromDB(int id);

        TutorialsAllData SearchTutorialsInDB(string tutorial);
    }
}