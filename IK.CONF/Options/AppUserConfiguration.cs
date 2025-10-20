using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IK.CONF.Options
{
    public class AppUserConfiguration:BaseConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);

            builder.HasOne(x => x.AppUserProfile).WithOne(x => x.AppUsers).HasForeignKey<AppUserProfile>(x => x.AppUserId);

            builder.HasOne(x => x.Employee).WithOne(x => x.AppUser).HasForeignKey<Employee>(x => x.AppUserId);
        }
    }
}
