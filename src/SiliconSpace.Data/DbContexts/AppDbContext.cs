using Microsoft.EntityFrameworkCore;
using SiliconSpace.Domain.Entities;
using SiliconSpace.Domain.Enums;

namespace SiliconSpace.Data.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public virtual DbSet<Booking> Bookings { get; set; }

        public virtual DbSet<Coworking> Coworkings { get; set; }

        public virtual DbSet<CoworkingZone> CoworkingZones { get; set; }

        public virtual DbSet<Organization> Organizations { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitly map entities to table names
            modelBuilder.Entity<Booking>().ToTable("Doc_Booking");
            modelBuilder.Entity<Coworking>().ToTable("Doc_Coworking");
            modelBuilder.Entity<CoworkingZone>().ToTable("Doc_Coworking_Zone");
            modelBuilder.Entity<Organization>().ToTable("Doc_Organization");
            modelBuilder.Entity<Role>().ToTable("Enum_Role");
            modelBuilder.Entity<Status>().ToTable("Enum_Status");
            modelBuilder.Entity<User>().ToTable("Sys_User");

            // Add any additional configuration below
        }

    }
}
