using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Abstraction.Contracts;
using Shared.Dtos.Auth_Module;

namespace Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles.Select(r => new RoleDto { Id = r.Id, Name = r.Name! });
        }

        public async Task<RoleDto?> GetRoleByIdAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return null;
            
            return new RoleDto { Id = role.Id, Name = role.Name! };
        }

        public async Task<RoleDto?> GetRoleByNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return null;
            
            return new RoleDto { Id = role.Id, Name = role.Name! };
        }

        public async Task<IdentityResult> CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            var roleExists = await _roleManager.RoleExistsAsync(createRoleDto.RoleName);
            if (roleExists)
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Description = $"Role '{createRoleDto.RoleName}' already exists." 
                });
            }

            var role = new IdentityRole(createRoleDto.RoleName);
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> UpdateRoleAsync(string roleId, UpdateRoleDto updateRoleDto)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Description = $"Role with ID '{roleId}' not found." 
                });
            }

            role.Name = updateRoleDto.NewRoleName;
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Description = $"Role with ID '{roleId}' not found." 
                });
            }

            return await _roleManager.DeleteAsync(role);
        }

        public async Task<IdentityResult> AssignRoleByEmailAsync(AssignRoleByEmailDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Description = $"User with email '{dto.Email}' not found." 
                });
            }

            if (!await _roleManager.RoleExistsAsync(dto.RoleName))
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Description = $"Role '{dto.RoleName}' does not exist." 
                });
            }

            if (await _userManager.IsInRoleAsync(user, dto.RoleName))
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Description = $"User already has role '{dto.RoleName}'." 
                });
            }

            return await _userManager.AddToRoleAsync(user, dto.RoleName);
        }

        public async Task<IdentityResult> AssignRoleByUsernameAsync(AssignRoleByUsernameDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Description = $"User with username '{dto.Username}' not found." 
                });
            }

            if (!await _roleManager.RoleExistsAsync(dto.RoleName))
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Description = $"Role '{dto.RoleName}' does not exist." 
                });
            }

            if (await _userManager.IsInRoleAsync(user, dto.RoleName))
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Description = $"User already has role '{dto.RoleName}'." 
                });
            }

            return await _userManager.AddToRoleAsync(user, dto.RoleName);
        }

        public async Task<IdentityResult> AssignRoleByAcademicCodeAsync(AssignRoleByAcademicCodeDto dto)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Academic_Code == dto.AcademicCode);
                
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Description = $"User with academic code '{dto.AcademicCode}' not found." 
                });
            }

            if (!await _roleManager.RoleExistsAsync(dto.RoleName))
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Description = $"Role '{dto.RoleName}' does not exist." 
                });
            }

            if (await _userManager.IsInRoleAsync(user, dto.RoleName))
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Description = $"User already has role '{dto.RoleName}'." 
                });
            }

            return await _userManager.AddToRoleAsync(user, dto.RoleName);
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new NotFoundException($"User with ID '{userId}' not found.");

            return await _userManager.GetRolesAsync(user);
        }
    }
}
