using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.BLL.Managers.Abstracts;
using IK.BLL.Managers.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace IK.BLL.DependencyResolvers
{
    public static class ManagerResolver
    {
        public static void AddManagerService(this IServiceCollection services)
        {
            services.AddScoped<IAppUserManager, AppUserManager>();
            services.AddScoped<IAppUserProfileManager, AppUserProfileManager>();
            services.AddScoped<IBranchManager, BranchManager>();
            services.AddScoped<ICareerPlanManager, CareerPlanManager>();
            services.AddScoped<IDepartmantManager, DepartmantManager>();
            services.AddScoped<IEmployeeManager, EmployeeManager>();
            services.AddScoped<IEmployeeQualificationManager, EmployeeQualificationManager>();
            services.AddScoped<IJobApplicationManager, JobApplicationManager>();
            services.AddScoped<IPayrollManager, PayrollManager>();
            services.AddScoped<IPositionManager, PositionManager>();
            services.AddScoped<IReportManager, ReportManager>();
            services.AddScoped<IWorkHourManager, WorkHourManager>();
            
        }
    }
}
