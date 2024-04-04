using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_CAN.Domain.Entities.User;

namespace project_CAN.BusinessLogic.DBContext
{
    public class DBSessionContext : DbContext
    {
        public DBSessionContext() : base("name=project_CAN_DB")
        {

        }

        public virtual DbSet<SessionDBTable> Sessions { get; set; }
    }
}
