using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IK.CONF.Options
{
    public class DepartmantConfiguration:BaseConfiguration<Departmant>
    {
        public override void Configure(EntityTypeBuilder<Departmant> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.DepartmantName).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Description).HasMaxLength(500);

            builder.HasOne(x => x.Branch).WithMany(x => x.Departmants).HasForeignKey(x => x.BranchId);
        }
    }
}
