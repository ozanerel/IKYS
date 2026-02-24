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
    public static class WorkHourSeed
    {
        public static void SeedWorkHours(ModelBuilder modelBuilder)
        {
            WorkHour wh1 = new()
            {
                Id = 1,
                EmployeeId = 1,
                EntryTime = new DateTime(2025, 11, 10, 9, 0, 0),
                ExitTime = new DateTime(2025, 11, 10, 18, 0, 0),
                Status = DataStatus.Approved
                //TotalHours = 9 //Entity tarafında encapsulation için set kaldırıldı bu sebepten ilk başta seed olarak kullanamayacağız.
            };


            modelBuilder.Entity<WorkHour>().HasData(wh1);
        }
    }
}
