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
    public class EmployeeConfiguration : BaseConfiguration<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.TCKN).HasMaxLength(11);
            builder.Property(x => x.Salary).HasColumnType("money");

            builder.HasOne(x => x.Departmant).WithMany(x => x.Employees).HasForeignKey(x => x.DepartmanId);
            builder.HasOne(x => x.Branch).WithMany(x => x.Employees).HasForeignKey(x => x.BranchId);
            builder.HasOne(x => x.Position).WithMany(x => x.Employees).HasForeignKey(x => x.PositionId);
            builder.HasOne(x => x.EmployeeQualification).WithOne(x => x.Employee).HasForeignKey<EmployeeQualification>(x => x.EmployeeId);
        }
    }
}
