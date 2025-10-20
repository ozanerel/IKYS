using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.ENTITIES.Models
{
    public class Position:BaseEntity
    {
        public string PositionName { get; set; }
        public string RequiredEducation { get; set; }
        public string RequiredExperience { get; set; }
        public decimal MaxSalary { get; set; }
        public decimal MinSalary { get; set; }

        public int DepartmantId { get; set; }

        //Relational Properties
        public Departmant Departmant { get; set; }
        public ICollection<Employee> Employees { get; set; }

    }
}
