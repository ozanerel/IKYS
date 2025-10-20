using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.ENTITIES.Models
{
    public class CareerPlan : BaseEntity
    {
        public DateTime PlannedPromotionDate { get; set; }
        public string Notes { get; set; }


        public int EmployeeId { get; set; }
        public int CurrentPositionId { get; set; }
        public int TargetPositionId { get; set; }

        //Relational Properties
        public Employee Employee { get; set; }
        public Position CurrentPosition { get; set; }
        public Position TargetPosition { get; set; }

    }

}
