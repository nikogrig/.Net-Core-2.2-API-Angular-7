using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rubi.Data.Configurations;
using Rubi.Data.Models;
using System;

namespace Rubi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Clinic> Clinics { get; set;  }

        public DbSet<ClinicManager> ClinicManagers { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Staff> StaffsList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationUserConfig());

            modelBuilder.ApplyConfiguration(new ClinicConfig());

            modelBuilder.ApplyConfiguration(new ClinicManagerConfig());

            modelBuilder.ApplyConfiguration(new DoctorConfig());

            modelBuilder.ApplyConfiguration(new PatientConfig());

            modelBuilder.ApplyConfiguration(new StaffConfig());

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");

            modelBuilder.Entity<ApplicationRole>().ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole<Guid>>(entity =>
            {
                entity.ToTable("UserRoles");
                entity.HasKey(key => new { key.UserId, key.RoleId });
            });

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");

            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens"); 

            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        }
    }
}
