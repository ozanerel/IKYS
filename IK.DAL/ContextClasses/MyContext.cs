using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.CONF.Options;
using IK.DAL.BogusHandling;
using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IK.DAL.ContextClasses
{
    public class MyContext:IdentityDbContext<AppUser,IdentityRole<int>,int>
    {
        public MyContext(DbContextOptions<MyContext> opt):base(opt)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new AppUserProfileConfiguration());
            builder.ApplyConfiguration(new BranchConfiguration());
            builder.ApplyConfiguration(new CareerPlanConfiguration());
            builder.ApplyConfiguration(new DepartmantConfiguration());
            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new EmployeeQualificationConfiguration());
            builder.ApplyConfiguration(new JobApplicationConfiguration());
            builder.ApplyConfiguration(new PayrollConfiguration());
            builder.ApplyConfiguration(new PositionConfiguration());
            builder.ApplyConfiguration(new ReportConfiguration());
            builder.ApplyConfiguration(new WorkHourConfiguration());

            UserAndRoleSeed.SeedUsersAndRoles(builder);
            BranchSeed.SeedBranches(builder);
            CareerPlanSeed.SeedCareerPlans(builder);
            DepartmantSeed.SeedDepartmants(builder);
            EmployeeQualificationSeed.SeedEmployeeQualifications(builder);
            EmployeeSeed.SeedEmployees(builder);
            JobApplicationSeed.SeedJobApplications(builder);
            PayrollSeed.SeedPayrolls(builder);
            PositionSeed.SeedPositions(builder);
            WorkHourSeed.SeedWorkHours(builder);

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserProfile> AppUserProfiles { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<CareerPlan> CareerPlans { get; set; }
        public DbSet<Departmant> Departmants { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeQualification> EmployeeQualifications { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<WorkHour> WorkHours { get; set; }
    }
}
