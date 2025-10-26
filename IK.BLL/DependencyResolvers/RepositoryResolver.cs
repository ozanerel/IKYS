using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.DAL.Repositories.Abstracts;
using IK.DAL.Repositories.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace IK.BLL.DependencyResolvers
{
    public static class RepositoryResolver
    {
        public static void AddRepositoryService(this IServiceCollection services)
        {
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IAppUserProfileRepository, AppUserProfileRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<ICareerPlanRepository, CareerPlanRepository>();
            services.AddScoped<IDepartmantRepository, DepartmantRepository>();
            services.AddScoped<IEmployeeQualificationRepository, EmployeeQualificationRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
            services.AddScoped<IPayrollRepository, PayrollRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IWorkHourRepository, WorkHourRepository>();
        }
    }
}
