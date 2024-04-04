﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_CAN.Domain.Entities.User;

namespace project_CAN.BusinessLogic.DBModel
{
    public class DBUserContext : DbContext
    {
        public DBUserContext() : base("name=project_CAN_DB")
        {

        }

        public virtual DbSet<UDBTable> Users { get; set; }
    }
}
