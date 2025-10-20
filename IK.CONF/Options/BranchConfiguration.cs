using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IK.CONF.Options
{
    public class BranchConfiguration: BaseConfiguration<Branch>
    {
        public override void Configure(EntityTypeBuilder<Branch> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.BranchName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Address).HasMaxLength(250);
            builder.Property(x=>x.City).HasMaxLength(50);


        }
    }
}
