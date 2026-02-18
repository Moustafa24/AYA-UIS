using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Entities.Models;
using AYA_UIS.Core.Domain.Enums;
using Shared.Dtos.Info_Module.CourseDtos;
using Shared.Dtos.Info_Module.SemesterDtos;
using Shared.Dtos.Info_Module.StdudyYearDtos;
using Shared.Dtos.Info_Module.UserDtos;
using Shared.Dtos.Info_Module.UserStudyYearDtos;

namespace Shared.Dtos.Info_Module.RegistrationDtos
{
    public class RegistrationDetailsDto
    {
        public int Id { get; set; }
        public RegistrationStatus Status { get; set; }
        public string? Reason { get; set; }
        public Grads? Grade { get; set; }
        public CourseDto Course { get; set; } = null!;
        public StudyYearDto StudyYearDto { get; set; } = null!;
        public SemesterDto Semester { get; set; } = null!;
        public UserDto User { get; set; } = null!;
        public CourseDto CourseDto { get; set; } = null!;
        public DateTime RegisteredAt { get; set; }
    }
}