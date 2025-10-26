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
    public class BranchRepository:BaseRepository<Branch>,IBranchRepository
    {
        public BranchRepository(MyContext context):base(context)
        {
            
        }
    }
}
