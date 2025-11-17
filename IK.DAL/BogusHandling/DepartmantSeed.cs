using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;

namespace IK.DAL.BogusHandling
{
    public static class DepartmantSeed
    {
        public static void SeedDepartmants(ModelBuilder modelBuilder)
        {
            Departmant dep1 = new() { Id = 1, DepartmantName = "İK", Description = "İnsan Kaynakları", BranchId = 1 };
            Departmant dep2 = new() { Id = 2, DepartmantName = "Yazılım", Description = "Yazılım Geliştirme", BranchId = 2 };
            modelBuilder.Entity<Departmant>().HasData(dep1, dep2);
        }
    }
}
