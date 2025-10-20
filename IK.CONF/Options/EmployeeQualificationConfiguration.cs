using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IK.CONF.Options
{
    public class EmployeeQualificationConfiguration:BaseConfiguration<EmployeeQualification>
    {
        public override void Configure(EntityTypeBuilder<EmployeeQualification> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.Employee).WithOne(x => x. EmployeeQualification).HasForeignKey<EmployeeQualification>(x => x.EmployeeId);
        }
    }
}
