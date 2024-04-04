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
//using project_CAN.BusinessLogic.DBModel;
//using project_CAN.Domain.Entities.User;
//using project_CAN.Helpers;

namespace project_CAN.BusinessLogic.Core
{
    public class UserApi
    {
        internal UResponseLogin UserLoginAction(ULoginData dataUserView)
        {
            UDBTable userTable;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(dataUserView.email))
            {
                //var pass = LoginHelper.HashGen(dataUserView.password);
                using (var db = new DBUserContext())
                {
                    //result = db.Users.FirstOrDefault(itemDB => itemDB.email == dataUserView.email && itemDB.password == pass);
                    userTable = db.Users.FirstOrDefault(itemDB => itemDB.email == dataUserView.email && itemDB.password == dataUserView.password);
                }

                if (userTable.isBlocked)
                {
                    return new UResponseLogin { Status = false, StatusMsg = "You are blocked!" };
                }

                if (userTable == null)
                {
                    return new UResponseLogin { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new DBUserContext())
                {
                    userTable.lastLogin = dataUserView.lastLogin;
                    todo.Entry(userTable).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new UResponseLogin { Status = true };
            }
            // When user logins with username
            else
            {
                //var pass = LoginHelper.HashGen(dataUserView.password);
                using (var db = new DBUserContext())
                {
                    //userTable = db.Users.FirstOrDefault(itemDB => itemDB.userName == dataUserView.userName && itemDB.password == pass);
                    userTable = db.Users.FirstOrDefault(itemDB => itemDB.userName == dataUserView.userName && itemDB.password == dataUserView.password);
                }

                if (userTable.isBlocked)
                {
                    return new UResponseLogin { Status = false, StatusMsg = "You are blocked!" };
                }

                if (userTable == null)
                {
                    return new UResponseLogin { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new DBUserContext())
                {
                    userTable.lastLogin = dataUserView.lastLogin;
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

            using (var db = new DBSessionContext())
            {
                SessionDBTable curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Sessions where e.userName == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.userName == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.cookieValue = apiCookie.Value;
                    curent.expireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new DBSessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new SessionDBTable
                    {
                        userName = loginCredential,
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
            SessionDBTable session;
            UDBTable curentUser;

            using (var db = new DBSessionContext())
            {
                session = db.Sessions.FirstOrDefault(itemDB => itemDB.cookieValue == cookie && itemDB.expireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new DBUserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.userName))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.email == session.userName);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.userName == session.userName);
                }
            }

            if (curentUser == null) return null;
            Mapper.Initialize(cfg => cfg.CreateMap<UDBTable, UserMinimal>());
            var userminimal = Mapper.Map<UserMinimal>(curentUser);

            return userminimal;
        }
    }
}
