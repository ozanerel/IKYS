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
                AppUserId = 1,
                DepartmanId = 1,
                PositionId = 1,
                BranchId = 1
            };

            Employee emp2 = new()
            {
                Id = 2,
                FirstName = "Ahmet",
                LastName = "Baykara",
                TCKN = "12345678902",
                BirthDate = new DateTime(1992, 5, 10),
                Gender = Gender.Male,
                MaritalStatus = MaritalStatus.Married,
                PhoneNumber = "05000000002",
                Email = "ahmet@ik.com",
                Address = "Ankara",
                StartDate = DateTime.Now.AddYears(-1),
                Salary = 9000,
                JobType = JobType.FullTime,
                AppUserId = 2,
                DepartmanId = 2,
                PositionId = 2,
                BranchId = 2
            };

            modelBuilder.Entity<Employee>().HasData(emp1, emp2);
        }
    }
}
