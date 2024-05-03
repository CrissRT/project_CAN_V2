using project_CAN.Domain.Entities.Moderator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using project_CAN.Domain.Entities.User;

namespace project_CAN.BusinessLogic.DBModel
{
    public class DBLikesContext: DbContext
    {
        public DBLikesContext() : base("name=project_CAN_DB")
        {
        }

        public virtual DbSet<LikesDBTable> Likes { get; set; }
    }
}
