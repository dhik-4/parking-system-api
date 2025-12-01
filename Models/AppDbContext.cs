using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ParkingSystemAPI.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MemberCard> MemberCards { get; set; }

    public virtual DbSet<ParkingFare> ParkingFares { get; set; }

    public virtual DbSet<TransactionParking> TransactionParkings { get; set; }

    public virtual DbSet<UnitMaster> UnitMasters { get; set; }

    public virtual DbSet<VehicleMaster> VehicleMasters { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=ParkingSystemLatihan;User Id=sa;Password=password1;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MemberCard>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__MemberCa__55FECD8E5C86892D");

            entity.ToTable("MemberCard");

            entity.Property(e => e.CardId).HasColumnName("CardID");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.VehicleMasterId).HasColumnName("VehicleMasterID");
        });

        modelBuilder.Entity<ParkingFare>(entity =>
        {
            entity.HasKey(e => e.ParkingFareId).HasName("PK__ParkingF__20620B1A95812BFB");

            entity.ToTable("ParkingFare");

            entity.Property(e => e.ParkingFareId).HasColumnName("ParkingFareID");
            entity.Property(e => e.Fare).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FareSchemeJson).HasColumnType("text");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.UnitMasterId).HasColumnName("UnitMasterID");
            entity.Property(e => e.VehicleMasterId).HasColumnName("VehicleMasterID");
        });

        modelBuilder.Entity<TransactionParking>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A4B48FE71EF");

            entity.ToTable("TransactionParking");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsMember).HasColumnName("isMember");
            entity.Property(e => e.PlateNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RefNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Tariff).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TimeIn).HasColumnType("datetime");
            entity.Property(e => e.TimeOut).HasColumnType("datetime");
            entity.Property(e => e.TotalPay).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UnitMasterId).HasColumnName("UnitMasterID");
            entity.Property(e => e.VehicleMasterId).HasColumnName("VehicleMasterID");
        });

        modelBuilder.Entity<UnitMaster>(entity =>
        {
            entity.HasKey(e => e.UnitMasterId).HasName("PK__UnitMast__36B7F420D727D4ED");

            entity.ToTable("UnitMaster");

            entity.Property(e => e.UnitMasterId).ValueGeneratedOnAdd();
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VehicleMaster>(entity =>
        {
            entity.HasKey(e => e.VehicleMasterId).HasName("PK__VehicleM__941D59AEDFE8E2F0");

            entity.ToTable("VehicleMaster");

            entity.Property(e => e.VehicleMasterId)
                .ValueGeneratedOnAdd()
                .HasColumnName("VehicleMasterID");
            entity.Property(e => e.Code)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.VehicleName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
