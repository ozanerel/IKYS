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
    public class PayrollConfiguration:BaseConfiguration<Payroll>
    {
        public override void Configure(EntityTypeBuilder<Payroll> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.GrossSalary).IsRequired().HasColumnType("money");
            builder.Property(x => x.NetSalary).IsRequired().HasColumnType("money");
            builder.Property(x => x.Bonuses).IsRequired().HasColumnType("money");

            builder.HasOne(x => x.Employee).WithMany(x => x.Payrolls).HasForeignKey(x => x.EmployeeId);
        }
    }
}
