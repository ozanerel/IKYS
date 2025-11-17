using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IK.DAL.BogusHandling
{
    public static class BranchSeed
    {
        public static void SeedBranches(ModelBuilder modelBuilder)
        {
            Branch branch1 = new() { Id = 1, BranchName = "Merkez Şube", City = "İstanbul", Address = "İstanbul Kadıköy" };
            Branch branch2 = new() { Id = 2, BranchName = "Ankara Şube", City = "Ankara", Address = "Ankara Etimesgut" };
            modelBuilder.Entity<Branch>().HasData(branch1, branch2);
        }
    }
}
