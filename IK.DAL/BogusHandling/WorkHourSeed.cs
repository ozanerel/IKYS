using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;

namespace IK.DAL.BogusHandling
{
    public static class WorkHourSeed
    {
        public static void SeedWorkHours(ModelBuilder modelBuilder)
        {
            WorkHour wh1 = new()
            {
                Id = 1,
                EmployeeId = 1,
                EntryTime = DateTime.Today.AddHours(9),
                ExitTime = DateTime.Today.AddHours(18),
                TotalHours = 9
            };

            WorkHour wh2 = new()
            {
                Id = 2,
                EmployeeId = 2,
                EntryTime = DateTime.Today.AddHours(9),
                ExitTime = DateTime.Today.AddHours(18),
                TotalHours = 9
            };

            modelBuilder.Entity<WorkHour>().HasData(wh1, wh2);
        }
    }
}
