using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.DAL.ContextClasses;
using IK.DAL.Repositories.Abstracts;
using IK.ENTITIES.Models;

namespace IK.DAL.Repositories.Concretes
{
    public class EmployeeQualificationRepository:BaseRepository<EmployeeQualification>,IEmployeeQualificationRepository
    {
        public EmployeeQualificationRepository(MyContext context):base(context)
        {
            
        }
    }
}
