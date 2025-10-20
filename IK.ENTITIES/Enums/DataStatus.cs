using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.ENTITIES.Enums
{
    public enum DataStatus
    {
        Inserted = 1,
        Updated = 2,
        Deleted = 3,
        Active = 4,
        Passive = 5,
        Pending = 6,
        Approved = 7
    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }

    public enum MaritalStatus
    {
        Single = 1,
        Married = 2,
        Divorced = 3,
        Widowed = 4
    }

    public enum EducationLevel
    {
        HighSchool = 1,
        AssociateDegree = 2,
        BachelorDegree = 3,
        MasterDegree = 4,
        Doctorate = 5
    }

    public enum JobType
    {
        FullTime = 1,
        PartTime = 2,
        Contract = 3,
        Internship = 4,
        Temporary = 5
    }

    public enum ApplicationStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
    }
}
