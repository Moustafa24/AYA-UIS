using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Entities.Models;
using Domain.enums;
using Microsoft.AspNetCore.Identity;

namespace AYA_UIS.Core.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Academic_Code { get; set; } = string.Empty;
        public Levels Level { get; set; } // if he study eng will prep year any other will start from first year
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
        public ICollection<AcademicSchedule> AcademicSchedules { get; set; } = new List<AcademicSchedule>();
        public ICollection<CourseUpload> CourseUpload { get; set; } = new List<CourseUpload>();

    }
}
