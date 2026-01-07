using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Auth_Module
{
    public record AssignRoleByAcademicCodeDto
    {
        [Required]
        public string AcademicCode { get; set; } = string.Empty;
        
        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}
