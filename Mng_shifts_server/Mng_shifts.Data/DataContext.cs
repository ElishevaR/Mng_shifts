//using Microsoft.EntityFrameworkCore;
//using Mng_shifts.Core.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Mng_shifts.Data
//{
//    public class DataContext: DbContext
//    {
//        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

//        public DbSet<Employee> Employee { get; set; }
//        public DbSet<Shift> Shifts { get; set; }
//        public DbSet<SwapRequest> SwapRequests { get; set; }
//        public DbSet<SwapProposal> SwapProposals { get; set; }
//    }
//}
using Microsoft.EntityFrameworkCore;
using Mng_shifts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mng_shifts.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<SwapRequest> SwapRequests { get; set; }
        public DbSet<SwapProposal> SwapProposals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SwapProposal>()
                .HasOne(p => p.ProposedShift)
                .WithMany()
                .HasForeignKey(p => p.ProposedShiftId)
                .OnDelete(DeleteBehavior.Restrict);
            // אם יש מפתחות זרים נוספים שיוצרים בעיה - תקבע עבורם גם Restrict או NoAction

            base.OnModelCreating(modelBuilder);
        }



    }
}
