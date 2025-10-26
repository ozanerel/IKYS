using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.BLL.Managers.Abstracts;
using IK.DAL.Repositories.Abstracts;
using IK.ENTITIES.Models;

namespace IK.BLL.Managers.Concretes
{
    public class EmployeeManager:BaseManager<Employee>,IEmployeeManager
    {
        readonly IEmployeeRepository _repository;
        public EmployeeManager(IEmployeeRepository repository):base(repository)
        {
            _repository = repository;
        }
    }
}
