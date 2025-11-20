using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.BLL.Managers.Abstracts;
using IK.DAL.Repositories.Abstracts;
using IK.ENTITIES.Enums;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;

namespace IK.BLL.Managers.Concretes
{
    public class BranchManager:BaseManager<Branch>,IBranchManager
    {
        readonly IBranchRepository _repository;
        readonly IEmployeeRepository _employeeRepository;
        readonly IDepartmantRepository _departmantRepository;
        public BranchManager(IBranchRepository repository,IEmployeeRepository employeeRepository,IDepartmantRepository departmantRepository):base(repository)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _departmantRepository = departmantRepository;

        }

        public async Task ActiveBranchAsync(int id)
        {
            var branch = await _repository.GetByIdAsync(id);
            if (branch == null) return;

            branch.Status = DataStatus.Active;
            await _repository.UpdateAsync(branch,branch);
        }

        public async Task AddDepartmantToBranchAsync(int branchId, Departmant departmant)
        {
            departmant.BranchId = branchId;
            await _departmantRepository.CreateAsync(departmant);
        }

        public async Task AssignEmployeeToBranchAsync(int employeeId, int branchId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null) return;

            employee.BranchId = branchId;
            employee.UpdatedDate = DateTime.Now;
            await _employeeRepository.UpdateAsync(employee,employee);
        }

        public async Task<List<Employee>> GetEmployeesByBranchAsync(int branchId)
        {
            return await _employeeRepository.Where(x => x.BranchId == branchId).ToListAsync();
        }
    }
}
