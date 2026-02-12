using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Dtos.Info_Module.CourseDtos
{
    public class CourseWithRegistrationsDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int StudyYearId { get; set; }
        public ICollection<StudentRegistrationDto> StudentRegistrations { get; set; } = new List<StudentRegistrationDto>();
    }
}
