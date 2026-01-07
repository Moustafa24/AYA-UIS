using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Auth_Module
{
    public record AssignRoleByEmailDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}
