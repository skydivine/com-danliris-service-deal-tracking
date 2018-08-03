using Com.DanLiris.Service.DealTracking.Lib.Models;
using Com.Moonlay.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Com.DanLiris.Service.DealTracking.Lib
{
    public class DealTrackingDbContext : StandardDbContext
    {
        public DealTrackingDbContext(DbContextOptions<DealTrackingDbContext> options) : base(options)
        {
        }

        /* Master */
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Reason> DealTrackingReasons { get; set; }

        public DbSet<Board> DealTrackingBoards { get; set; }
        public DbSet<Stage> DealTrackingStages { get; set; }
        public DbSet<Deal> DealTrackingDeals { get; set; }
        public DbSet<Activity> DealTrackingActivities { get; set; }
        public DbSet<ActivityAttachment> DealTrackingActivityAttachments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
