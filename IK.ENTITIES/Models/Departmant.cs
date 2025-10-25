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
        public virtual Branch Branch { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
    }
}
