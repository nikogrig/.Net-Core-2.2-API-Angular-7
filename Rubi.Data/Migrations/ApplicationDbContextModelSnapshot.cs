﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Rubi.Data;

namespace Rubi.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Rubi.Data.Models.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Rubi.Data.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<DateTime>("Birthdate")
                        .HasMaxLength(2018);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id")
                        .HasName("UserId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Rubi.Data.Models.Clinic", b =>
                {
                    b.Property<Guid>("ClinicId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClinicName")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.HasKey("ClinicId");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("Rubi.Data.Models.ClinicManager", b =>
                {
                    b.Property<Guid>("ClinicManagerId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClinicId");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("ClinicManagerId");

                    b.HasIndex("ClinicId")
                        .IsUnique();

                    b.ToTable("ClinicManagers");
                });

            modelBuilder.Entity("Rubi.Data.Models.Doctor", b =>
                {
                    b.Property<Guid>("DoctorId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClinicId");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Profession")
                        .IsRequired();

                    b.Property<Guid?>("UserId");

                    b.HasKey("DoctorId");

                    b.HasIndex("ClinicId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Rubi.Data.Models.Patient", b =>
                {
                    b.Property<Guid>("PatientId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClinicId");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("Illness")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<Guid?>("UserId");

                    b.HasKey("PatientId");

                    b.HasIndex("ClinicId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Rubi.Data.Models.Staff", b =>
                {
                    b.Property<Guid>("StaffId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClinicId");

                    b.Property<string>("Description");

                    b.HasKey("StaffId");

                    b.HasIndex("ClinicId");

                    b.ToTable("StaffsList");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Rubi.Data.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Rubi.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Rubi.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Rubi.Data.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Rubi.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Rubi.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Rubi.Data.Models.ClinicManager", b =>
                {
                    b.HasOne("Rubi.Data.Models.Clinic", "Clinic")
                        .WithOne("Manager")
                        .HasForeignKey("Rubi.Data.Models.ClinicManager", "ClinicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Rubi.Data.Models.Doctor", b =>
                {
                    b.HasOne("Rubi.Data.Models.Clinic", "Clinic")
                        .WithMany("Doctors")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Rubi.Data.Models.ApplicationUser", "AppClient")
                        .WithOne("Doctor")
                        .HasForeignKey("Rubi.Data.Models.Doctor", "UserId");
                });

            modelBuilder.Entity("Rubi.Data.Models.Patient", b =>
                {
                    b.HasOne("Rubi.Data.Models.Clinic", "Clinic")
                        .WithMany("Patients")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Rubi.Data.Models.ApplicationUser", "AppClient")
                        .WithOne("Patient")
                        .HasForeignKey("Rubi.Data.Models.Patient", "UserId");
                });

            modelBuilder.Entity("Rubi.Data.Models.Staff", b =>
                {
                    b.HasOne("Rubi.Data.Models.Clinic", "Clinic")
                        .WithMany("StaffList")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
