using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rubi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rubi.Data.Configurations
{
    public class ClinicManagerConfig : IEntityTypeConfiguration<ClinicManager>
    {
        public void Configure(EntityTypeBuilder<ClinicManager> entity)
        {
            entity.HasKey(a => a.ClinicManagerId);
        }
    }
}