using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using project_CAN.BusinessLogic.Core;
using project_CAN.BusinessLogic.Interfaces;
using project_CAN.Domain.Entities.Admin;
using project_CAN.Domain.Entities.Moderator;
using project_CAN.Domain.Entities.User;

namespace project_CAN.BusinessLogic
{
    public class AdminBL : AdminApi, IAdmin
    {
        public int CountAllUserLikesFromDb(int userId)
        {
            return CountAllUserLikes(userId);
        }

        public LikesAllData GetLikedTutorialsFromDB(int userId)
        {
            return GetAllUserLikes(userId);
        }

        public OperationOnUserResponse EditUserInDB(UserEdit data)
        {
            return EditUser(data);
        }
        public UsersAllData GetAllUsersExceptAdminFromDB(int excludeId)
        {
            return GetAllUsers(excludeId);
        }

        public void DeleteUserFromDB(int id)
        {
            DeleteUser(id);
        }

        public UDBTable GetUserByIdFromDB(int id)
        {
            return GetUserById(id);
        }

        public TutorialResponse AddContentInDB(TutorialDomainData data, string pathImagesTutorial)
        {
            return AddContent(data, pathImagesTutorial);
        }

        public TutorialsAllData GetAllTutorialFromDB()
        {
            return GetAllTutorial();
        }

        public void RemoveTutorialFromDB(int id, string pathImagesTutorial)
        {
            RemoveTutorial(id, pathImagesTutorial);
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

        public bool LikeAndDislikeinDB(LikesData data)
        {
            return LikeAndDislike(data);
        }

        public DBTutorialTable GetTutorialByIdFromDB(int id)
        {
            return GetTutorialById(id);
        }

        public TutorialResponse EditTutorialInDB(TutorialDomainData data, string pathImagesTutorial)
        {
            return EditTutorial(data, pathImagesTutorial);
        }
    }
}
