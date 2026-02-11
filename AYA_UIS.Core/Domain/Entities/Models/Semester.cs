using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Entities;
using AYA_UIS.Core.Domain.Entities.Models;
using Domain.enums;

namespace Domain.Entities.Models
{
    public class Semester : BaseEntities<int>
    {
        public SemseterEnums Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public ICollection<AcademicSchedule> AcademicSchedules { get; set; } = new List<AcademicSchedule>();
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
        public ICollection<SemesterGPA> SemesterGPAs { get; set; } = new List<SemesterGPA>();
    }
}