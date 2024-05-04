using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using AutoMapper;
using project_CAN.BusinessLogic.DBModel;
using project_CAN.Domain.Entities.Admin;
using project_CAN.Domain.Entities.User;
using project_CAN.Helpers;

namespace project_CAN.BusinessLogic.Core
{
    public class AdminApi : ModeratorApi
    {
        internal void DeleteUser(int id)
        {
            using (var db = new DBSessionContext())
            {
                // Delete all sessions associated with the user
                var sessions = db.Sessions.Where(s => s.userId == id).ToList();
                foreach (var session in sessions)
                {
                    db.Sessions.Remove(session);
                }
                db.SaveChanges();
            }

            using (var db = new DBLikesContext())
            {
                var likes = db.Likes.Where(l => l.userId == id).ToList();
                foreach (var like in likes)
                {
                    db.Likes.Remove(like);
                }
                db.SaveChanges();
            }

            using (var db = new DBUserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.userId == id);
                if (user == null) return;

                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        internal OperationOnUserResponse EditUser(UserEdit data)
        {
            using (var db = new DBUserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.userId == data.userId);

                // Update user properties only if they are not null
                if (data.userName != null)
                {
                    user.userName = data.userName;
                }

                if (data.userName != null && (data.userName.Length < 3 || data.userName.Length > 50))
                {
                    return new OperationOnUserResponse { Status = false, StatusMsg = "Ați introdus lungimea incorectă a username-ului" };
                }
                var validate = new EmailAddressAttribute();
                if (data.email != null)
                {
                    user.email = data.email;
                }

                if (data.email != null && !validate.IsValid(data.email))
                {
                    return new OperationOnUserResponse { Status = false, StatusMsg = "Ați introdus format incorect a email-ului" };
                }


                if (data.password != null)
                {
                    var pass = LoginHelper.HashGen(data.password);
                    user.password = pass;
                }

                if (data.password != null && (data.password.Length < 8 || data.password.Length > 50))
                {
                    return new OperationOnUserResponse { Status = false, StatusMsg = "Ați introdus lungimea incorectă a parolei" };
                }

                user.privilegies = data.privilegies;
                user.isBlocked = data.isBlocked;
                db.SaveChanges();
            }

            return new OperationOnUserResponse { Status = true, StatusMsg = "Datele utilizatorului au fost editate cu succes" };
        }

        internal UDBTable GetUserById(int id)
        {
            UDBTable user = null;
            using (var db = new DBUserContext())
            {
                user = db.Users.FirstOrDefault(itemDB => itemDB.userId == id);
            }

            return user;
        }

        internal UsersAllData GetAllUsers(int excludeId)
        {
            using (var db = new DBUserContext())
            {
                var allUsersExceptAdmin = db.Users.Where(u => u.userId != excludeId).ToList();
                return new UsersAllData { UsersData = allUsersExceptAdmin };
            }
        }
    }
}
