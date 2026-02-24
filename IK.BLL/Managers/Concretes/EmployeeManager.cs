using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.BLL.Managers.Abstracts;
using IK.DAL.Repositories.Abstracts;
using IK.ENTITIES.Enums;
using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;

namespace IK.BLL.Managers.Concretes
{
    public class EmployeeManager:BaseManager<Employee>,IEmployeeManager
    {
        readonly IEmployeeRepository _repository;
        readonly UserManager<AppUser> _userManager;
        readonly IPositionManager _positionManager;
        public EmployeeManager(IEmployeeRepository repository,UserManager<AppUser> userManager,IPositionManager positionManager):base(repository)
        {
            _repository = repository;
            _userManager = userManager;
            _positionManager = positionManager;
        }

        public async Task ChangeDepartmentASync(int employeeId, int newDepartmantId)
        {
            var employee =await _repository.GetByIdAsync(employeeId);
            if (employee == null) return;

            employee.DepartmanId = newDepartmantId;
            await _repository.UpdateAsync(employee,employee);
        }

        public async Task CreateAsync(Employee employee)
        {
            await _repository.CreateAsync(employee);
        }

        public async Task HireFromJobApplication(JobApplication app, string userName, int departmanId, int branchId)
        {
            if (string.IsNullOrWhiteSpace(app.Email))
                throw new Exception("Başvuru email bilgisi boş.");

            var existingUser = await _userManager.FindByEmailAsync(app.Email);
            if (existingUser != null)
                throw new Exception("Bu email ile zaten kullanıcı mevcut.");

            var user = new AppUser
            {
                UserName = userName,
                Email = app.Email,
                EmailConfirmed = true,
                CreatedDate = DateTime.Now,
                Status = DataStatus.Inserted
            };

            var result = await _userManager.CreateAsync(user, "Employee123*");

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(x => x.Description)));

            await _userManager.AddToRoleAsync(user, "Employee");

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
                DepartmanId = departmanId,
                BranchId = branchId
            };

            await _repository.CreateAsync(employee);
        }

        public async Task UpdateAsync(Employee employee)
        {
            var original = await _repository.GetByIdAsync(employee.Id);
            if (original == null) return;

            // izin verilen alanlar
            original.FirstName = employee.FirstName;
            original.LastName = employee.LastName;
            original.Email = employee.Email;
            original.PhoneNumber = employee.PhoneNumber;
            original.Salary = employee.Salary;
            original.DepartmanId = employee.DepartmanId;
            original.PositionId = employee.PositionId;
            original.BranchId = employee.BranchId;

            // repository Update original üzerine yazacak
            await _repository.UpdateAsync(original, employee);
        }

        public async Task UpdateSalaryAsync(int employeeId, decimal newSalary)
        {
            var employee = await _repository.GetByIdAsync(employeeId);
            if (employee == null) return;

            employee.Salary = newSalary;
            await _repository.UpdateAsync(employee,employee);
        }
    }
}
