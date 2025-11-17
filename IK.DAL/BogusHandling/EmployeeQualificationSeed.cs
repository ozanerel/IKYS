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
    public static class EmployeeQualificationSeed
    {
        public static void SeedEmployeeQualifications(ModelBuilder modelBuilder)
        {
            EmployeeQualification eq1 = new()
            {
                Id = 1,
                Education = "Üniversite",
                EducationLevel =EducationLevel.BachelorDegree ,
                Experience = "2 yıl",
                Skills = "MS Office",
                Languages = "İngilizce",
                Certifications = "İK Sertifikası",
                EmployeeId = 1
            };

            EmployeeQualification eq2 = new()
            {
                Id = 2,
                Education = "Üniversite",
                EducationLevel = EducationLevel.BachelorDegree,
                Experience = "3 yıl",
                Skills = "C#, ASP.NET",
                Languages = "İngilizce",
                Certifications = "MCP",
                EmployeeId = 2
            };

            modelBuilder.Entity<EmployeeQualification>().HasData(eq1, eq2);
        }
    }
}
