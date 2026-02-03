using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.BLL.Managers.Abstracts;
using IK.BLL.Managers.Concretes;
using IK.BLL.Services.Abstracts;
using IK.ENTITIES.Enums;
using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;

namespace IK.BLL.Services.Concretes
{
    public class EmployeeHireService:IEmployeeHireService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IPositionManager _positionManager;
        public EmployeeHireService(UserManager<AppUser> userManager,IEmployeeManager employeeManager,IPositionManager positionManager)
        {
            _userManager = userManager;
            _employeeManager = employeeManager;
            _positionManager = positionManager;
        }

        public async Task HireFromJobApplication(JobApplication app,string userName, int departmantId, int branchId)
        {
            //DepartmaId gelmediği için kontrol
            //Console.WriteLine($"BranchId : {branchId},DepartmantId : {departmantId}");

            //Email kontrol
            if (string.IsNullOrWhiteSpace(app.Email))
                throw new Exception("Başvuru email bilgisi boş.");

            var existingUser = await _userManager.FindByEmailAsync(app.Email);
            if (existingUser != null)
                throw new Exception($"Bu email ile zaten bir kullanıcı var: {app.Email}");


            // 1) AppUser oluştur
            var user = new AppUser
            {
                UserName = userName,
                Email = app.Email,
                EmailConfirmed = true,
                CreatedDate = DateTime.Now,
                Status = DataStatus.Inserted
            };

            string tempPassword = "Employee123*";

            var result = await _userManager.CreateAsync(user, tempPassword);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }

            await _userManager.AddToRoleAsync(user, "Employee");

            // 2) Position → Departman → Branch çöz
            var position = await _positionManager.GetByIdAsync(app.PositionId);

            //int departmantId = position.DepartmantId;
            //int branchId = position.Departmant.BranchId;

            // 3) Employee oluştur
            var employee = new Employee
            {
                FirstName = app.ApplicantName.Split(" ")[0],
                LastName = app.ApplicantName.Contains(" ")
                    ? app.ApplicantName.Split(" ").Last()
                    : "-",
                TCKN = app.TCKN,

                Email = app.Email,
                PhoneNumber = app.PhoneNumber,
                Address = app.Address,
                StartDate = DateTime.Today,
                BirthDate = (DateTime)app.BirthDate,
                Salary = (decimal)app.Salary,
                Gender = (Gender)app.Gender,
                MaritalStatus = (MaritalStatus)app.MaritalStatus,
                JobType = (JobType)app.JobType,

                AppUserId = user.Id,
                PositionId = app.PositionId,
                DepartmanId = departmantId,
                BranchId = branchId
            };

            await _employeeManager.CreateAsync(employee);
        }
    }
}

