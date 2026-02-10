using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Entities.Identity;

namespace AYA_UIS.Core.Domain.Entities.Models
{
    public class Department : BaseEntities<int>
    {

        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<StudyYear> StudyYears { get; set; } = new List<StudyYear>();
        public ICollection<DepartmentFee> DepartmentFees { get; set; } = new List<DepartmentFee>();
        public ICollection<AcademicSchedule> AcademicSchedules { get; set; } = new List<AcademicSchedule>();
        public ICollection<User> Users { get; set; } = new List<User>();

    }
}
