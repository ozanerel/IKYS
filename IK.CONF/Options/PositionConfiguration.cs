using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IK.CONF.Options
{
    public class PositionConfiguration:BaseConfiguration<Position>
    {
        public override void Configure(EntityTypeBuilder<Position> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.PositionName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.MaxSalary).HasColumnType("money");
            builder.Property(x => x.MinSalary).HasColumnType("money");

            builder.HasOne(x => x.Departmant).WithMany(x => x.Positions).HasForeignKey(x => x.DepartmantId);

        }
    }
}
