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
    public static class JobApplicationSeed
    {
        public static void SeedJobApplications(ModelBuilder modelBuilder)
        {
            JobApplication ja1 = new()
            {
                Id = 1,
                ApplicantName = "Mehmet Demir",
                Email = "mehmet@example.com",
                PhoneNumber = "05001112233",
                ApplicateDate = DateTime.Now.AddDays(-10),
                ApplicationStatus = ApplicationStatus.Pending,
                CVFilePath = "CVs/MehmetDemir.pdf",
                PositionId = 2
            };

            JobApplication ja2 = new()
            {
                Id = 2,
                ApplicantName = "Ayşe Yılmaz",
                Email = "ayse@example.com",
                PhoneNumber = "05004445566",
                ApplicateDate = DateTime.Now.AddDays(-5),
                ApplicationStatus = ApplicationStatus.Approved,
                CVFilePath = "CVs/AyseYilmaz.pdf",
                PositionId = 1
            };

            modelBuilder.Entity<JobApplication>().HasData(ja1, ja2);
        }
    }
}
