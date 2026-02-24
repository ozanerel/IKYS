using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Enums;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;

namespace IK.DAL.BogusHandling
{
    public static class EmployeeSeed
    {
        public static void SeedEmployees(ModelBuilder modelBuilder)
        {
            Employee emp1 = new()
            {
                Id = 1,
                FirstName = "Ozan",
                LastName = "Erel",
                TCKN = "12345678901",
                BirthDate = new DateTime(2002, 8, 20),
                Gender = Gender.Male,
                MaritalStatus = MaritalStatus.Single,
                PhoneNumber = "05000000001",
                Email = "ozan@ik.com",
                Address = "İstanbul",
                StartDate = DateTime.Now.AddYears(-2),
                Salary = 10000,
                JobType = JobType.FullTime,
                ImagePath = "/images/default.png",
                AppUserId = 1,
                DepartmanId = 1,
                PositionId = 1,
                BranchId = 1
            };


            modelBuilder.Entity<Employee>().HasData(emp1);
        }
    }
}
