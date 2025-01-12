using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SiliconSpace.Shared;

public partial class SiliconSpaceContext : DbContext
{
    public SiliconSpaceContext()
    {
    }

    public SiliconSpaceContext(DbContextOptions<SiliconSpaceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DocBooking> DocBookings { get; set; }

    public virtual DbSet<DocCoworking> DocCoworkings { get; set; }

    public virtual DbSet<DocCoworkingZone> DocCoworkingZones { get; set; }

    public virtual DbSet<DocOrganization> DocOrganizations { get; set; }

    public virtual DbSet<EnumRole> EnumRoles { get; set; }

    public virtual DbSet<EnumStatus> EnumStatuses { get; set; }

    public virtual DbSet<SysUser> SysUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=26122005;Database=SiliconSpace;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DocBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Doc_Booking_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.CoworkingZone).WithMany(p => p.DocBookings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Doc_Booking_CoworkingZoneId_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.DocBookings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Doc_Booking_StatusId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.DocBookings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Doc_Booking_UserId_fkey");
        });

        modelBuilder.Entity<DocCoworking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Doc_Coworking_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Organization).WithMany(p => p.DocCoworkings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Doc_Coworking_OrganizationId_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.DocCoworkings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Doc_Coworking_StatusId_fkey");
        });

        modelBuilder.Entity<DocCoworkingZone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Doc_Coworking_Zone_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Coworking).WithMany(p => p.DocCoworkingZones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Doc_Coworking_Zone_CoworkingId_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.DocCoworkingZones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Doc_Coworking_Zone_StatusId_fkey");
        });

        modelBuilder.Entity<DocOrganization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Doc_Organization_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Role).WithMany(p => p.DocOrganizations).HasConstraintName("Doc_Organization_RoleId_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.DocOrganizations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Doc_Organization_StatusId_fkey");
        });

        modelBuilder.Entity<EnumRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Enum_Roles_pkey");
        });

        modelBuilder.Entity<EnumStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Enum_Status_pkey");
        });

        modelBuilder.Entity<SysUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Sys_User_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Role).WithMany(p => p.SysUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Sys_User_RoleId_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.SysUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Sys_User_StatusId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
