using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_CAN.BusinessLogic.Core;
using project_CAN.BusinessLogic.Interfaces;
using project_CAN.Domain.Entities.Admin;
using project_CAN.Domain.Entities.User;

namespace project_CAN.BusinessLogic
{
    public class AdminBL : AdminApi, IAdmin
    {
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

        public ContentResponse AddContentInDB(ContentDomainData data, string pathImagesContent)
        {
            return AddContent(data, pathImagesContent);
        }

        public TutorialsAllData GetAllContentFromDB()
        {
            return GetAllContent();
        }
    }
}
