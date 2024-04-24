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
        protected internal UResponse UserRegistrationAction(URegistrationData dataUserDomain)
        {
            UDBTable userTable;

            var validate = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            if (!validate.IsValid(dataUserDomain.email))
            {
                return new UResponse { Status = false, StatusMsg = "Email format incorect" };
            }

            if (dataUserDomain.username.Length < 3)
            {
                return new UResponse { Status = false, StatusMsg = "Username trebuie sa contina minim 3 caractere" };
            }

            if (dataUserDomain.username.Length > 50)
            {
                return new UResponse { Status = false, StatusMsg = "Username trebuie sa contina maxim 50 caractere" };
            }

            if (dataUserDomain.password.Length > 50)
            {
                return new UResponse { Status = false, StatusMsg = "Parola trebuie sa contina maxim 50 caractere" };
            }

            if (dataUserDomain.password.Length < 8)
            {
                return new UResponse { Status = false, StatusMsg = "Parola trebuie sa contina minim 8 caractere" };
            }

            if (dataUserDomain.email.Length > 256)
            {
                return new UResponse { Status = false, StatusMsg = "Email trebuie sa contina maxim 256 caractere" };
            }


            var pass = LoginHelper.HashGen(dataUserDomain.password);

            if (LoginHelper.HashGen(dataUserDomain.repeatPassword) != pass)
            {
                return new UResponse { Status = false, StatusMsg = "Repeta Parola incorect" };
            }
            
            using (var db = new DBUserContext())
            {
                if (db.Users.Any(itemDB => itemDB.email == dataUserDomain.email) || db.Users.Any(itemDB => itemDB.userName == dataUserDomain.username))
                {
                    return new UResponse {Status = false, StatusMsg = "Utilizatorul deja exista"};
                }

                var newUser = new UDBTable
                {
                    userName = dataUserDomain.username,
                    email = dataUserDomain.email,
                    password = pass,
                    privilegies = URole.user,
                    lastLogin = DateTime.Now,
                    isBlocked = false
                };

                db.Users.Add(newUser);
                db.SaveChanges();
            }
            return new UResponse { Status = true, StatusMsg = "Registrare cu success"};
        }
        protected internal UResponse UserLoginAction(ULoginData dataUserDomain)
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
                    return new UResponse { Status = false, StatusMsg = "Login-ul sau Parola este incorecta" };
                }

                if (userTable.isBlocked)
                {
                    return new UResponse { Status = false, StatusMsg = "D-voastra sunteti blocat!" };
                }

                using (var todo = new DBUserContext())
                {
                    userTable.lastLogin = DateTime.Now;
                    todo.Entry(userTable).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new UResponse { Status = true , StatusMsg = "Login cu success"};
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
                    return new UResponse { Status = false, StatusMsg = "Login-ul sau Parola este incorecta" };
                }

                if (userTable.isBlocked)
                {
                    return new UResponse { Status = false, StatusMsg = "D-voastra sunteti blocat!" };
                }

                using (var todo = new DBUserContext())
                {
                    userTable.lastLogin = dataUserDomain.lastLogin;
                    todo.Entry(userTable).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new UResponse { Status = true , StatusMsg = "Login cu success" };
            }
        }

        protected internal HttpCookie Cookie(string loginCredential)
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


        protected internal UResponse EditProfile(EditProfile data)
        {
            UDBTable currentUser = GetUserFromCookie(data.apiCookie);
            if (currentUser == null) return new UResponse { Status = false, StatusMsg = "Sesiunea utilizatorul nu a fost gasit!"};

            using (var db = new DBUserContext())
            {
                var changesList = new List<string>();
                if (data.userName != null && data.userName.Length > 3 && data.userName.Length <= 50)
                {
                    changesList.Add("username-ului");
                    currentUser.userName = data.userName;
                }
                var validate = new EmailAddressAttribute();
                if (data.email != null && validate.IsValid(data.email))
                {
                    changesList.Add("email-ului");
                    currentUser.email = data.email;
                }

                if (data.password != null && data.password.Length > 8 && data.password.Length <= 50)
                {
                    changesList.Add(" parolei");
                    currentUser.password = data.password;
                }
                string changes= string.Join(", ", changesList.ToArray());
                db.Entry(currentUser).State = EntityState.Modified;
                db.SaveChanges();
                return new UResponse { Status = true, StatusMsg = $"Schimbare salvata a {changes}!" };
            }
        }

        private UDBTable GetUserFromCookie(string cookie)
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
            return currentUser;
        }

        protected internal UserMinimal UserCookie(string cookie)
        {
            UDBTable currentUser = GetUserFromCookie(cookie);
            if (currentUser == null) return null;
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<UDBTable, UserMinimal>());
            return Mapper.Map<UserMinimal>(currentUser);
        }
    }
}
