using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IK.DAL.BogusHandling
{
    public static class UserAndRoleSeed
    {
        public static void SeedUsersAndRoles(ModelBuilder modelBuilder)
        {
            IdentityRole<int> appRole = new()
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };

            modelBuilder.Entity<IdentityRole<int>>().HasData(appRole);

            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();

            AppUser appUser = new()
            {
                Id = 1,
                UserName = "admin",
                Email = "admin@ik.com",
                NormalizedEmail ="ADMIN@IK.COM",
                NormalizedUserName = "ADMIN",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = passwordHasher.HashPassword(null, "Admin1234"),
            };

            modelBuilder.Entity<AppUser>().HasData(appUser);

            IdentityUserRole<int> appUserRole = new()
            {
                RoleId = 1,
                UserId = 1,
            };

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(appUserRole);



            IdentityRole<int> employeeRole = new()
            {
                Id = 2,
                Name = "Employee",
                NormalizedName = "EMPLOYEE",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };

            modelBuilder.Entity<IdentityRole<int>>().HasData(employeeRole);
            PasswordHasher<AppUser> passwordHasher2 = new PasswordHasher<AppUser>();

            AppUser appUser2 = new()
            {
                Id = 2,
                UserName = "ozan",
                Email = "ozan@ik.com",
                NormalizedEmail = "OZAN@IK.COM",
                NormalizedUserName = "OZAN",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = passwordHasher.HashPassword(null, "Ozan1234"),
            };

            modelBuilder.Entity<AppUser>().HasData(appUser2);

            IdentityUserRole<int> appUserRole2 = new()
            {
                RoleId = 2,
                UserId = 2,
            };

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(appUserRole2);
        }
    }
}
