using System.Data.Entity;

namespace project_CAN.BusinessLogic.DBModel
{
    public class DBImageContext : DbContext
    {
        public DBImageContext() : base("name=project_CAN_DB")
        {

        }

        //public virtual DbSet<UDBTable> Users { get; set; }
    }
}
