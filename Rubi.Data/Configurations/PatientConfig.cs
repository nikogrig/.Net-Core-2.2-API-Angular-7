using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rubi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rubi.Data.Configurations
{
    class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> entity)
        {
            entity.HasKey(p => p.PatientId);

            entity
                .HasOne(u => u.AppClient)
                .WithOne(p => p.Patient)
                .HasForeignKey<Patient>(p => p.UserId);
        }
    }
}