using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rubi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rubi.Data.Configurations
{
    class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> entity)
        {
            entity.HasKey(a => a.DoctorId);

            entity
      .HasOne(u => u.AppClient)
      .WithOne(d => d.Doctor)
      .HasForeignKey<Doctor>(d => d.UserId);
        }
    }
}