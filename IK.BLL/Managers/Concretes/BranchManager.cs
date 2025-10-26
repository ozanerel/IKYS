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
    public class BranchManager:BaseManager<Branch>,IBranchManager
    {
        readonly IBranchRepository _repository;
        public BranchManager(IBranchRepository repository):base(repository)
        {
            _repository = repository;
            
            
        }
    }
}
