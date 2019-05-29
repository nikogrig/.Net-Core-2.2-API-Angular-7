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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
