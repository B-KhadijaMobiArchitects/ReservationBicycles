using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

public partial class LocationVeloDbContext : IdentityDbContext<IdentityUser, IdentityRole,string>
{
    public LocationVeloDbContext()
    {
    }

    public LocationVeloDbContext(DbContextOptions<LocationVeloDbContext> options)
        : base(options)
    {
    }

    //public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    //public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Bicyclette> Bicyclettes { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<LocationBicyclette> LocationBicyclettes { get; set; }

    public virtual DbSet<ModePaiement> ModePaiements { get; set; }

    public virtual DbSet<ModeleBicyclette> ModeleBicyclettes { get; set; }

    public virtual DbSet<StatutBicyclette> StatutBicyclettes { get; set; }

    public virtual DbSet<StatutReservation> StatutReservations { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=LocationVeloDb;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<AspNetRole>(entity =>
        //{
        //    entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
        //        .IsUnique()
        //        .HasFilter("([NormalizedName] IS NOT NULL)");

        //    entity.Property(e => e.Name).HasMaxLength(256);
        //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
        //});

        //modelBuilder.Entity<AspNetRoleClaim>(entity =>
        //{
        //    entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

        //    entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        //});

        //modelBuilder.Entity<AspNetUser>(entity =>
        //{
        //    entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

        //    entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
        //        .IsUnique()
        //        .HasFilter("([NormalizedUserName] IS NOT NULL)");

        //    entity.Property(e => e.Email).HasMaxLength(256);
        //    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
        //    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
        //    entity.Property(e => e.UserName).HasMaxLength(256);

        //    entity.HasMany(d => d.Roles).WithMany(p => p.Users)
        //        .UsingEntity<Dictionary<string, object>>(
        //            "AspNetUserRole",
        //            r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
        //            l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
        //            j =>
        //            {
        //                j.HasKey("UserId", "RoleId");
        //                j.ToTable("AspNetUserRoles");
        //                j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
        //            });
        //});

        //modelBuilder.Entity<AspNetUserClaim>(entity =>
        //{
        //    entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

        //    entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        //});

        //modelBuilder.Entity<AspNetUserLogin>(entity =>
        //{
        //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

        //    entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

        //    entity.Property(e => e.LoginProvider).HasMaxLength(128);
        //    entity.Property(e => e.ProviderKey).HasMaxLength(128);

        //    entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        //});

        //modelBuilder.Entity<AspNetUserToken>(entity =>
        //{
        //    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

        //    entity.Property(e => e.LoginProvider).HasMaxLength(128);
        //    entity.Property(e => e.Name).HasMaxLength(128);

        //    entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        //});

        modelBuilder.Entity<Bicyclette>(entity =>
        {
            entity.ToTable("Bicyclette");

            entity.Property(e => e.DateAchat).HasColumnType("datetime");
            entity.Property(e => e.Libelle).HasMaxLength(50);

            entity.HasOne(d => d.EtatNavigation).WithMany(p => p.Bicyclettes)
                .HasForeignKey(d => d.Etat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bicyclette_Bicyclette");

            entity.HasOne(d => d.ModeleNavigation).WithMany(p => p.Bicyclettes)
                .HasForeignKey(d => d.Modele)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bicyclette_ModeleBicyclette");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.DateCreation).HasColumnType("datetime");
        });

        modelBuilder.Entity<LocationBicyclette>(entity =>
        {
            entity.ToTable("LocationBicyclette");

            entity.Property(e => e.DateDebut).HasColumnType("datetime");
            entity.Property(e => e.DateFin).HasColumnType("datetime");
            entity.Property(e => e.DateRetour).HasColumnType("datetime");

            entity.HasOne(d => d.Bicyclette).WithMany(p => p.LocationBicyclettes)
                .HasForeignKey(d => d.BicycletteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LocationBicyclette_LocationBicyclette");

            entity.HasOne(d => d.Client).WithMany(p => p.LocationBicyclettes)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LocationBicyclette_Client");

            entity.HasOne(d => d.ModePaiementNavigation).WithMany(p => p.LocationBicyclettes)
                .HasForeignKey(d => d.ModePaiement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LocationBicyclette_ModePaiement");

            entity.HasOne(d => d.Statut).WithMany(p => p.LocationBicyclettes)
                .HasForeignKey(d => d.StatutId)
                .HasConstraintName("FK_LocationBicyclette_Bicyclette");
        });

        modelBuilder.Entity<ModePaiement>(entity =>
        {
            entity.ToTable("ModePaiement");

            entity.Property(e => e.Libelle).HasMaxLength(50);
        });

        modelBuilder.Entity<ModeleBicyclette>(entity =>
        {
            entity.ToTable("ModeleBicyclette");

            entity.Property(e => e.Code).HasMaxLength(50);
        });

        modelBuilder.Entity<StatutBicyclette>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EtatBicyclette");

            entity.ToTable("StatutBicyclette");

            entity.Property(e => e.Code).HasMaxLength(50);
        });

        modelBuilder.Entity<StatutReservation>(entity =>
        {
            entity.ToTable("StatutReservation");

            entity.Property(e => e.Code).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
