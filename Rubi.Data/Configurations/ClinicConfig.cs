using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rubi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rubi.Data.Configurations
{
    public class ClinicConfig : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> entity)
        {
            entity.HasKey(c => c.ClinicId);

            entity
              .HasOne(c => c.Manager)
              .WithOne(cm => cm.Clinic)
              .HasForeignKey<ClinicManager>(cm => cm.ClinicId);

            entity
              .HasMany(c => c.Doctors)
              .WithOne(d => d.Clinic)
              .HasForeignKey(d => d.ClinicId);

            entity
              .HasMany(c => c.StaffList)
              .WithOne(s => s.Clinic)
              .HasForeignKey(s => s.ClinicId);

            entity
             .HasMany(c => c.Patients)
             .WithOne(p => p.Clinic)
             .HasForeignKey(p => p.ClinicId);
        }
    }
}
