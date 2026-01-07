using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Auth_Module
{
    public record CreateRoleDto
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}
