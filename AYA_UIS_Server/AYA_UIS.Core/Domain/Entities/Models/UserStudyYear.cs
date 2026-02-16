using System;
using AYA_UIS.Core.Domain.Entities.Identity;
using AYA_UIS.Core.Domain.Enums;

namespace AYA_UIS.Core.Domain.Entities.Models
{
    /// <summary>
    /// Tracks a user's enrollment in a specific study year, recording which academic level
    /// they were at during that year. This provides a full history from enrollment to graduation.
    /// </summary>
    public class UserStudyYear : BaseEntities<int>
    {
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;

        public int StudyYearId { get; set; }
        public StudyYear StudyYear { get; set; } = null!;

        public Levels Level { get; set; } // The academic level of the student in this study year

        public bool IsCurrent { get; set; } // Whether this is the student's current active study year

        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    }
}
