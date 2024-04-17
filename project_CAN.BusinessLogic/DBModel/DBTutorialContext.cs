using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace project_CAN.BusinessLogic.DBModel
{
    public class DBTutorialContext : DbContext
    {
        public DBTutorialContext() : base("name=project_CAN_DB")
        {
        }

        //public virtual DbSet<UDBTable> Users { get; set; }
    }

    {
    }
}
