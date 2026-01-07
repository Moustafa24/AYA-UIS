using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Auth_Module
{
    public record AssignRoleByUsernameDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}
