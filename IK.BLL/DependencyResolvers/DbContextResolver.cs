using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using IK.DAL.ContextClasses;

namespace IK.BLL.DependencyResolvers
{
    public static class DbContextResolver
    {
        public static void AddDbContextService(this IServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();

            IConfiguration configuration = provider.GetRequiredService<IConfiguration>();


            services.AddDbContext<MyContext>(x => x.UseSqlServer(configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies());
        }
    }
}
