using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IK.CONF.Options
{
    public class WorkHourConfiguration: BaseConfiguration<WorkHour>
    {
        public override void Configure(EntityTypeBuilder<WorkHour> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.Employee).WithMany(x => x.WorkHours).HasForeignKey(x => x.EmployeeId);
        }
    }
}
