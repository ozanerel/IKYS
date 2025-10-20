using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.ENTITIES.Models
{
    public class Report:BaseEntity
    {
        public string ReportName { get; set; }
        public string ReportType { get; set; }
        public string FilePath { get; set; }
    }
}
