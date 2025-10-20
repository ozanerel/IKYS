using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.ENTITIES.Models
{
    public class Payroll:BaseEntity
    {
        //Bordro:Çalışan maaş detaylarını içerir
        public string Period { get; set; }//Örnek "2024-06"
        public decimal GrossSalary { get; set; }//Brüt Maaş
        public decimal NetSalary { get; set; }
        public decimal Bonuses { get; set; }
        public decimal Deductions { get; set; }//Kesintiler

        public int EmployeeId { get; set; }

        //Relational Properties
        public Employee Employee { get; set; }

    }
}
