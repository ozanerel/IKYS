using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.DAL.ContextClasses;
using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IK.BLL.DependencyResolvers
{
    public static class IdentityResolver
    {
        public static void AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole<int>>(x =>
            {
                x.Password.RequireDigit = false;
                x.Password.RequiredLength = 3;
                x.Password.RequireLowercase = false;
                x.Password.RequireUppercase = false;
                x.SignIn.RequireConfirmedEmail = false;
                x.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<MyContext>();
        }
    }
}
