using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.DAL.ContextClasses;
using IK.DAL.Repositories.Abstracts;
using IK.ENTITIES.Enums;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;

namespace IK.DAL.Repositories.Concretes
{
    public class PayrollRepository : BaseRepository<Payroll>, IPayrollRepository
    {
        readonly MyContext _context;
        public PayrollRepository(MyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Payroll>> GetPayrollsWithEmployeeAsync()
        {
            return await _context.Payrolls
        .Where(p => p.Status != DataStatus.Deleted)
        .Include(p => p.Employee)
        .ToListAsync();
        }
    }
}
