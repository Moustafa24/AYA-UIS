using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Entities;
using AYA_UIS.Core.Domain.Entities.Identity;
using Domain.Entities.Models;
using Domain.enums;

namespace AYA_UIS.Core.Domain.Entities.Models
{
    public class Registration : BaseEntities<int>
    {
        public Statuses Status { get; set; }
        public string? Reason { get; set; } // the reason for pending or canceling the registration
        public Grads Grade { get; set; }
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public int StudyYearId { get; set; }
        public StudyYear StudyYear { get; set; } = null!;
        public int SemesterId { get; set; }
        public Semester Semester { get; set; } = null!;
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}