using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rubi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rubi.Data.Configurations
{
    class StaffConfig : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> entity)
        {
            entity.HasKey(a => a.StaffId);


        }
    }
}