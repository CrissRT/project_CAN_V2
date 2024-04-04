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
using project_CAN.BusinessLogic.DBModel;
using project_CAN.Domain.Entities.User;
using project_CAN.Helpers;

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

                if (userTable == null)
                {
                    return new UResponseLogin { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new DBUserContext())
                {
                    userTable.LastLogin = dataUserView.LoginDateTime;
                    todo.Entry(userTable).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new UResponseLogin { Status = true };
            }
            else
            {
                var pass = LoginHelper.HashGen(dataUserView.password);
                using (var db = new DBUserContext())
                {
                    userTable = db.Users.FirstOrDefault(itemDB => itemDB.userName == dataUserView.userName && u.Password == pass);
                }

                if (userTable == null)
                {
                    return new UResponseLogin { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new DBUserContext())
                {
                    userTable.LastLogin = dataUserView.LoginDateTime;
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
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new DBSessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }

        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            UDbTable curentUser;

            using (var db = new DBSessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new DBUserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
                }
            }

            if (curentUser == null) return null;
            Mapper.Initialize(cfg => cfg.CreateMap<UDbTable, UserMinimal>());
            var userminimal = Mapper.Map<UserMinimal>(curentUser);

            return userminimal;
        }
    }
}

