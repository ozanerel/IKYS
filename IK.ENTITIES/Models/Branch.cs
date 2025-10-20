using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.ENTITIES.Models
{
    public class Branch:BaseEntity
    {
        //Şube
        public string BranchName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        //Relational Properties
        public ICollection<Departmant> Departmants { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
