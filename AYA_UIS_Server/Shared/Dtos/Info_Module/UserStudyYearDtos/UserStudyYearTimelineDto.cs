using AYA_UIS.Core.Domain.Enums;

namespace Shared.Dtos.Info_Module.UserStudyYearDtos
{
    /// <summary>
    /// Full timeline of a user's study years from enrollment to graduation.
    /// </summary>
    public class UserStudyYearTimelineDto
    {
        public string UserId { get; set; } = string.Empty;
        public Levels CurrentLevel { get; set; }
        public string CurrentLevelName { get; set; } = string.Empty;
        public int? CurrentStudyYearId { get; set; }
        public int? CurrentStartYear { get; set; }
        public int? CurrentEndYear { get; set; }
        public int TotalYearsCompleted { get; set; }
        public bool IsGraduated { get; set; }
        public List<UserStudyYearDto> StudyYears { get; set; } = new();
    }
}
