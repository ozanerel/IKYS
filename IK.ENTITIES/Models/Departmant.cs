using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.ENTITIES.Models
{
    public class Departmant:BaseEntity
    {
        //Departman
        public string DepartmantName { get; set; }
        public string Description { get; set; }

        public int BranchId { get; set; }

        //Relational Properties
        public Branch Branch { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Position> Positions { get; set; }
    }
}
