using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;

namespace IK.DAL.BogusHandling
{
    public static class CareerPlanSeed
    {
        public static void SeedCareerPlans(ModelBuilder modelBuilder)
        {
            CareerPlan cp1 = new()
            {
                Id = 1,
                EmployeeId = 1,
                CurrentPositionId = 1,
                TargetPositionId = 1,
                PlannedPromotionDate = DateTime.Now.AddMonths(6),
                Notes = "Başarılı performans"
            };

            CareerPlan cp2 = new()
            {
                Id = 2,
                EmployeeId = 2,
                CurrentPositionId = 2,
                TargetPositionId = 2,
                PlannedPromotionDate = DateTime.Now.AddMonths(12),
                Notes = "Tecrübeyi artıracak"
            };

            modelBuilder.Entity<CareerPlan>().HasData(cp1, cp2);
        }
    }
}
