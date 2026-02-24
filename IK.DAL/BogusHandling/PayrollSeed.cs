using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;

namespace IK.DAL.BogusHandling
{
    public static class PayrollSeed
    {
        public static void SeedPayrolls(ModelBuilder modelBuilder)
        {
            Payroll pay1 = new()
            {
                Id = 1,
                EmployeeId = 1,
                Period = "2025-11",
                TotalHours = 160,
                HourlyRate = 100,
                TaxRate = 0.1m,
                Bonuses = 500,
                //GrossSalary = 10000,
                //NetSalary = 9000,
                //Bonuses = 500,
                //Deductions = 500
            };


            modelBuilder.Entity<Payroll>().HasData(pay1);
        }
    }
}
