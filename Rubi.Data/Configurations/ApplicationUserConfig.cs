using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rubi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rubi.Data.Configurations
{
    class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> entity)
        {
            entity.ToTable("Users")
                  .HasKey(c => c.Id)
                  .HasName("UserId");
        }
    }
}