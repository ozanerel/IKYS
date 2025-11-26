using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.DAL.ContextClasses;
using IK.DAL.Repositories.Abstracts;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;

namespace IK.DAL.Repositories.Concretes
{
    public class EmployeeRepository:BaseRepository<Employee>,IEmployeeRepository
    {
        private readonly MyContext _context;
        public EmployeeRepository(MyContext context):base(context)
        {
            _context = context;
        }

        override public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.AppUser)
                .Include(e => e.Departmant)
                .Include(e => e.Position)
                .Include(e => e.Branch)
                .FirstOrDefaultAsync(e => e.Id == id);

        }
        
    
            
    }
}
