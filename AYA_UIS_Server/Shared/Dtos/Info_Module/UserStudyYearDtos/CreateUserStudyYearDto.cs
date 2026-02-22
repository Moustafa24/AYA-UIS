using System.ComponentModel.DataAnnotations;
using AYA_UIS.Core.Domain.Enums;

namespace Shared.Dtos.Info_Module.UserStudyYearDtos
{
    public class CreateUserStudyYearDto
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int StudyYearId { get; set; }

        [Required]
        public Levels Level { get; set; }
    }
}
