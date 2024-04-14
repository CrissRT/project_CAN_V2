using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_CAN.BusinessLogic.DBModel;
using project_CAN.Domain.Entities.Admin;

namespace project_CAN.BusinessLogic.Core
{
    public class AdminApi
    {
        public void AddUser()
        {
            // Add user logic here
        }

        public void DeleteUser()
        {
            // Delete user logic here
        }

        public void UpdateUser()
        {
            // Update user logic here
        }

        public void GetUser()
        {
            // Get user logic here
        }

        public UsersAllData GetAllUsers()
        {
            using (var db = new DBUserContext())
            {
                return new UsersAllData { UsersData = db.Users.ToList() };
            }
        }
    }
}
