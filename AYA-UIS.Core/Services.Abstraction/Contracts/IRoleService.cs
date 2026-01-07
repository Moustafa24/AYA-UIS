using Microsoft.AspNetCore.Identity;
using Shared.Dtos.Auth_Module;

namespace Services.Abstraction.Contracts
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto?> GetRoleByIdAsync(string roleId);
        Task<RoleDto?> GetRoleByNameAsync(string roleName);
        Task<IdentityResult> CreateRoleAsync(CreateRoleDto createRoleDto);
        Task<IdentityResult> UpdateRoleAsync(string roleId, UpdateRoleDto updateRoleDto);
        Task<IdentityResult> DeleteRoleAsync(string roleId);
        
        // Assign role methods
        Task<IdentityResult> AssignRoleByEmailAsync(AssignRoleByEmailDto dto);
        Task<IdentityResult> AssignRoleByUsernameAsync(AssignRoleByUsernameDto dto);
        Task<IdentityResult> AssignRoleByAcademicCodeAsync(AssignRoleByAcademicCodeDto dto);
        
        // Get user roles
        Task<IEnumerable<string>> GetUserRolesAsync(string userId);
    }
}
