using System.Data.Entity;
using project_CAN.Domain.Entities.Admin;
using project_CAN.Domain.Entities.User;

namespace project_CAN.BusinessLogic.DBModel
{
    public class DBImageContext : DbContext
    {
        public DBImageContext() : base("name=project_CAN_DB")
        {

        }

        public virtual DbSet<DBImageTable> Images { get; set; }
    }
}
