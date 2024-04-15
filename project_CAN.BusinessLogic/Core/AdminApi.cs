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

        public void DeleteUser(int id)
        {
            using (var db = new DBSessionContext())
            {
                var sessions = db.Sessions.Where(s => s.userId == id).ToList();
                // Remove each session from the DbSet
                foreach (var session in sessions)
                {
                    db.Sessions.Remove(session);
                }
            }

            using (var db = new DBUserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.userId == id);
                if (user == null) return;

                db.Users.Remove(user);
                db.SaveChanges();
            }
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
