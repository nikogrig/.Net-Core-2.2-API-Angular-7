using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rubi.Data.Configurations;
using Rubi.Data.Models;

namespace Rubi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<IdentityUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");

            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
                //entity.HasKey(key => new { key.UserId, key.RoleId });
            });

            //modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserToken");

            //modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");

            //modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");

            //modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");

            modelBuilder.Ignore<IdentityUserToken<string>>();

            modelBuilder.Ignore<IdentityUserClaim<string>>();

            modelBuilder.Ignore<IdentityUserLogin<string>>();

            modelBuilder.Ignore<IdentityRoleClaim<string>>();

            modelBuilder.ApplyConfiguration(new ApplicationUserConfig());
        }
    }
}
