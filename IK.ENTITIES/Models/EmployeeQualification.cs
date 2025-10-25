using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Enums;

namespace IK.ENTITIES.Models
{
    public class EmployeeQualification:BaseEntity
    {
        //Çalışan Nitelikleri
        public string Education { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public string Experience { get; set; }
        public string Skills { get; set; }
        public string Languages { get; set; }
        public string Certifications { get; set; }

        public int EmployeeId { get; set; }

        //Relational Properties
        public virtual Employee Employee { get; set; }
    }
}
