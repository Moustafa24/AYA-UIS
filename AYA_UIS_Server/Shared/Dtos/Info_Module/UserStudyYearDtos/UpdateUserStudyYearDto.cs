using AYA_UIS.Core.Domain.Enums;

namespace Shared.Dtos.Info_Module.UserStudyYearDtos
{
    public class UpdateUserStudyYearDto
    {
        public Levels? Level { get; set; }
        public bool? IsCurrent { get; set; }
    }
}
