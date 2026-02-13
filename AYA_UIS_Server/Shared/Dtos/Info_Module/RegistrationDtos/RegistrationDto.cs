using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Enums;

namespace Shared.Dtos.Info_Module.RegistrationDtos
{
    public class RegistrationDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public Statuses Status { get; set; }
        public string? Reason { get; set; }
        public Grads? Grade { get; set; }
        public int CourseCredits { get; set; }
        public int StudyYearId { get; set; }
        public int SemesterId { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}