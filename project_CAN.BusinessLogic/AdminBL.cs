using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_CAN.BusinessLogic.Core;
using project_CAN.BusinessLogic.Interfaces;
using project_CAN.Domain.Entities.Admin;

namespace project_CAN.BusinessLogic
{
    public class AdminBL : AdminApi, IAdmin
    {
        public UsersAllData GetAllUsersFromDB()
        {
            return GetAllUsers();
        }

        public void DeleteUserFromDB(int id)
        {
            DeleteUser(id);
        }
    }
}
