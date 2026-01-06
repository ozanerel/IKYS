using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.BLL.Services.Abstracts;
using IK.BLL.Services.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace IK.BLL.DependencyResolvers
{
    public static class ServiceResolver
    {
        public static void AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeHireService,EmployeeHireService>();
            services.AddScoped<IPayrollPdfService, PayrollPdfService>();
        }
    }
}
