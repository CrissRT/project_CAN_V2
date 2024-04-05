using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using project_CAN.Domain.Entities.User;

namespace project_CAN.BusinessLogic.DBModel
{
    public class DBSessionContext : DbContext
    {
        public DBSessionContext() : base("name=project_CAN_DB")
        {

        }

        public virtual DbSet<SessionDBTable> Sessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SessionDBTable>()
                .HasRequired(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.userId); // Configure foreign key

            base.OnModelCreating(modelBuilder);
        }

    }
}
