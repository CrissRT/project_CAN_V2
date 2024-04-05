using AutoMapper;
using project_CAN.BusinessLogic.DBModel;
using project_CAN.Domain.Entities.User;
using project_CAN.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace project_CAN.BusinessLogic.Core
{
    public class UserApi
    {
        internal UResponseLogin UserLoginAction(ULoginData dataUserDomain)
        {
            UDBTable userTable;
            var validate = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            if (validate.IsValid(dataUserDomain.credential))
            {
                //var pass = LoginHelper.HashGen(dataUserDomain.password);
                using (var db = new DBUserContext())
                {
                    //result = db.Users.FirstOrDefault(itemDB => itemDB.email == dataUserDomain.email && itemDB.password == pass);
                    //userTable = db.GetUserByEmailAndPassword(dataUserDomain.credential, dataUserDomain.password);
                    userTable = db.Users.FirstOrDefault(itemDB => itemDB.email == dataUserDomain.credential);
                }

                if (userTable == null)
                {
                    return new UResponseLogin { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                if (userTable.isBlocked)
                {
                    return new UResponseLogin { Status = false, StatusMsg = "You are blocked!" };
                }

                using (var todo = new DBUserContext())
                {
                    userTable.lastLogin = dataUserDomain.lastLogin;
                    todo.Entry(userTable).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new UResponseLogin { Status = true };
            }
            // When user logins with username
            else
            {
                //var pass = LoginHelper.HashGen(dataUserDomain.password);
                using (var db = new DBUserContext())
                {
                    //userTable = db.Users.FirstOrDefault(itemDB => itemDB.userName == dataUserDomain.userName && itemDB.password == pass);
                    userTable = db.Users.FirstOrDefault(itemDB => itemDB.userName == dataUserDomain.credential && itemDB.password == dataUserDomain.password);
                }

                if (userTable == null)
                {
                    return new UResponseLogin { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                if (userTable.isBlocked)
                {
                    return new UResponseLogin { Status = false, StatusMsg = "You are blocked!" };
                }

                using (var todo = new DBUserContext())
                {
                    userTable.lastLogin = dataUserDomain.lastLogin;
                    todo.Entry(userTable).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new UResponseLogin { Status = true };
            }
        }

        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };
            SessionDBTable currentSession = null;
            UDBTable currentUserNoSession = null;
            using (var db = new DBSessionContext())
            {
                // If user logins wih Email
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    currentSession = db.Sessions.FirstOrDefault(itemDB => itemDB.User.email == loginCredential);
                }
                // If user logins wih Username
                else
                {
                    currentSession = db.Sessions.FirstOrDefault(itemDB => itemDB.User.userName == loginCredential);

                }

                // If currentSession exists
                if (currentSession != null)
                {
                    currentSession.cookieValue = apiCookie.Value;
                    currentSession.expireTime = DateTime.Now.AddMinutes(60);
                    db.Entry(currentSession).State = EntityState.Modified;
                    db.SaveChanges(); // Save changes here
                }
                // If currentSession does not exist
                else
                {
                    using (var dbUser = new DBUserContext())
                    {
                        // If user logins wih Email
                        if (validate.IsValid(loginCredential))
                        {
                            currentUserNoSession = dbUser.Users.FirstOrDefault(itemDB => itemDB.email == loginCredential);
                        }
                        // If user logins wih Username
                        else
                        {
                            currentUserNoSession = dbUser.Users.FirstOrDefault(itemDB => itemDB.userName == loginCredential);
                        }
                    }
                    db.Sessions.Add(new SessionDBTable
                    {
                        userId = currentUserNoSession.userId,
                        cookieValue = apiCookie.Value,
                        expireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }

        internal UserMinimal UserCookie(string cookie)
        {
            SessionDBTable session = null;
            UDBTable currentUser = null;

            using (var db = new DBSessionContext())
            {
                session = db.Sessions.Include(s => s.User)
                                     .FirstOrDefault(itemDB => itemDB.cookieValue == cookie && itemDB.expireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new DBUserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.User.email))
                {
                    currentUser = db.Users.FirstOrDefault(u => u.email == session.User.email);
                }
                else
                {
                    currentUser = db.Users.FirstOrDefault(u => u.userName == session.User.userName);
                }
            }

            if (currentUser == null) return null;
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<UDBTable, UserMinimal>());
            var userminimal = Mapper.Map<UserMinimal>(currentUser);

            return userminimal;
        }

    }
}
