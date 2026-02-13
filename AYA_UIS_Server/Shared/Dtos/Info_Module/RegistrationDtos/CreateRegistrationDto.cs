
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Dtos.Info_Module.RegistrationDtos
{
    public class CreateRegistrationDto
    {
        public int CourseId { get; set; }
        public int StudyYearId { get; set; }
        public int SemesterId { get; set; }
    }
}