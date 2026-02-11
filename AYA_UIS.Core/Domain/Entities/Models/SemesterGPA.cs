using System;
using AYA_UIS.Core.Domain.Entities;
using AYA_UIS.Core.Domain.Entities.Identity;
using Domain.Entities.Models;

namespace AYA_UIS.Core.Domain.Entities.Models
{
    public class SemesterGPA : BaseEntities<int>
    {
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;
        
        public int SemesterId { get; set; }
        public Semester Semester { get; set; } = null!;
        
        public int StudyYearId { get; set; }
        public StudyYear StudyYear { get; set; } = null!;
        
        public decimal GPA { get; set; } // e.g., 3.85
        public int TotalCreditHours { get; set; }
        
        public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
    }
}
