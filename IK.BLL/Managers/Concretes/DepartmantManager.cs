using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.BLL.Managers.Abstracts;
using IK.DAL.Repositories.Abstracts;
using IK.DAL.Repositories.Concretes;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;

namespace IK.BLL.Managers.Concretes
{
    public class DepartmantManager:BaseManager<Departmant>,IDepartmantManager
    {
        readonly IDepartmantRepository _repository;
        readonly IEmployeeRepository _employeeRepository;
        readonly IPositionRepository _positionRepository;
        public DepartmantManager(IDepartmantRepository repository,IEmployeeRepository employeeRepository,IPositionRepository positionRepository):base(repository)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _positionRepository = positionRepository;
        }

        public async Task AddEmployeeToDepartmantAsync(int employeeId, int departmantId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null) return;

            employee.DepartmanId = departmantId;
            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task<List<Position>> GetPositionsInDepartmantAsync(int departmantId)
        {
            return await _positionRepository.Where(p => p.DepartmantId == departmantId).ToListAsync();
        }

    }
}
