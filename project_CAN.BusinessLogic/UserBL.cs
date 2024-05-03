using project_CAN.BusinessLogic.Core;
using project_CAN.BusinessLogic.Interfaces;
using project_CAN.Domain.Entities.User;
using System.Web;
using project_CAN.Domain.Entities.Moderator;

namespace project_CAN.BusinessLogic
{
    public class UserBL : UserApi, IUser
    {
        public TutorialsAllData GetAllTutorialFromDB()
        {
            return GetAllTutorial();
        }

        public UResponse UserLoginSessionBL(ULoginData data)
        {
            return UserLoginAction(data);
        }

        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }

        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }

        public UResponse UserRegistrationSessionBL(URegistrationData data)
        {
            return UserRegistrationAction(data);
        }

        public UResponse EditProfileFromDB(EditProfile data)
        {
            return EditProfile(data);
        }


        public void LikeAndDislikeinDB(LikesData data)
        {
            LikeAndDislike(data);
        }

        public int CountAllUserLikesFromDb(int userId)
        {
            return CountAllUserLikes(userId);
        }
    }
}
