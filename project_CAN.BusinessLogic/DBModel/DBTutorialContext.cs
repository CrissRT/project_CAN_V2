﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using project_CAN.Domain.Entities.Admin;
using project_CAN.Domain.Entities.Moderator;

namespace project_CAN.BusinessLogic.DBModel
{
    public class DBTutorialContext : DbContext
    {
        public DBTutorialContext() : base("name=project_CAN_DB")
        {
        }

        public virtual DbSet<DBTutorialTable> Tutorial { get; set; }
    }

}
