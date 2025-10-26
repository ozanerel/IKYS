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
    public class PayrollManager:BaseManager<Payroll>,IPayrollManager
    {
        readonly IPayrollRepository _repository;
        public PayrollManager(IPayrollRepository repository):base(repository)
        {
            _repository = repository;
            
            
        }
    }
}
