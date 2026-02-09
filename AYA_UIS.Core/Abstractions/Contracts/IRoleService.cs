using Microsoft.AspNetCore.Identity;
using Shared.Dtos.Auth_Module;

namespace AYA_UIS.Core.Abstractions.Contracts
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
        Task<IdentityResult> UpdateUserRoleByEmailAsync(UpdateUserRoleByEmailDto dto);

        // Get user roles
        Task<IEnumerable<string>> GetUserRolesAsync(string userId);

        // UpdateUserRoleByAcademicCode
        Task<IdentityResult> UpdateUserRoleByAcademicCodeAsync(UpdateUserRoleDto dto);

        // Get User Role Info By AcademicCode
        Task<UserRoleInfoDto> GetUserRoleInfoByAcademicCodeAsync(string academicCode);


    }
}
