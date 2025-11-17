using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;

namespace IK.DAL.BogusHandling
{
    public static class PositionSeed
    {
        public static void SeedPositions(ModelBuilder modelBuilder)
        {
            Position pos1 = new() { Id = 1, PositionName = "İK Uzmanı", RequiredEducation = "Üniversite", RequiredExperience = "2 yıl", MinSalary = 8000, MaxSalary = 12000, DepartmantId = 1 };
            Position pos2 = new() { Id = 2, PositionName = "Yazılım Geliştirici", RequiredEducation = "Üniversite", RequiredExperience = "3 yıl", MinSalary = 9000, MaxSalary = 15000, DepartmantId = 2 };
            modelBuilder.Entity<Position>().HasData(pos1, pos2);
        }
    }
}
