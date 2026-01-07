using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Auth_Module
{
    public record UpdateRoleDto
    {
        [Required]
        public string NewRoleName { get; set; } = string.Empty;
    }
}
