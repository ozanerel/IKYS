using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.ENTITIES.Models
{
    public class WorkHour:BaseEntity 
    {
        //Mesai
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public decimal TotalHours { get; set; }

        public int EmployeeId { get; set; }

        //Relational Properties
        public virtual Employee Employee { get; set; }
    }
}
