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
using project_CAN.Domain.Enums;

namespace project_CAN.BusinessLogic.Core
{
    public class UserApi
    {
        internal UResponseLogin UserRegistrationAction(URegistrationData dataUserDomain)
        {
            UDBTable userTable;

            var validate = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            if (!validate.IsValid(dataUserDomain.email))
            {
                return new UResponseLogin { Status = false, StatusMsg = "Email format incorect" };
            }
            var pass = LoginHelper.HashGen(dataUserDomain.password);

            if (LoginHelper.HashGen(dataUserDomain.repeatPassword) != pass)
            {
                return new UResponseLogin { Status = false, StatusMsg = "Repeta Parola incorect" };
            }
            
            using (var db = new DBUserContext())
            {
                if (db.Users.Any(itemDB => itemDB.email == dataUserDomain.email) || db.Users.Any(itemDB => itemDB.userName == dataUserDomain.username))
                {
                    return new UResponseLogin {Status = false, StatusMsg = "Utilizatorul deja exista"};
                }

                var newUser = new UDBTable
                {
                    userName = dataUserDomain.username,
                    email = dataUserDomain.email,
                    password = pass,
                    privilegies = URole.user,
                    lastLogin = dataUserDomain.lastLogin,
                    isBlocked = false
                };

                db.Users.Add(newUser);
                db.SaveChanges();
            }
            return new UResponseLogin { Status = true, StatusMsg = "Registrare cu success"};
        }
        internal UResponseLogin UserLoginAction(ULoginData dataUserDomain)
        {
            UDBTable userTable;
            var pass = LoginHelper.HashGen(dataUserDomain.password);
            var validate = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            if (validate.IsValid(dataUserDomain.credential))
            {
                using (var db = new DBUserContext())
                {
                    userTable = db.Users.FirstOrDefault(itemDB => itemDB.email == dataUserDomain.credential && itemDB.password == pass);
                }

                if (userTable == null)
                {
                    return new UResponseLogin { Status = false, StatusMsg = "Login-ul sau Parola este incorecta" };
                }

                if (userTable.isBlocked)
                {
                    return new UResponseLogin { Status = false, StatusMsg = "D-voastra sunteti blocat!" };
                }

                using (var todo = new DBUserContext())
                {
                    userTable.lastLogin = DateTime.Now;
                    todo.Entry(userTable).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new UResponseLogin { Status = true , StatusMsg = "Login cu success"};
            }
            // When user logins with username
            else
            {
                //var pass = LoginHelper.HashGen(dataUserDomain.password);
                using (var db = new DBUserContext())
                {
                    userTable = db.Users.FirstOrDefault(itemDB => itemDB.userName == dataUserDomain.credential && itemDB.password == pass);
                }

                if (userTable == null)
                {
                    return new UResponseLogin { Status = false, StatusMsg = "Login-ul sau Parola este incorecta" };
                }

                if (userTable.isBlocked)
                {
                    return new UResponseLogin { Status = false, StatusMsg = "D-voastra sunteti blocat!" };
                }

                using (var todo = new DBUserContext())
                {
                    userTable.lastLogin = dataUserDomain.lastLogin;
                    todo.Entry(userTable).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new UResponseLogin { Status = true , StatusMsg = "Login cu success" };
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
