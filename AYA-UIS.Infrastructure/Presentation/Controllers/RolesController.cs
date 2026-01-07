using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.Auth_Module;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public RolesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoles()
        {
            var roles = await _serviceManager.RoleService.GetAllRolesAsync();
            return Ok(roles);
        }

        /// <summary>
        /// Get role by ID
        /// </summary>
        [HttpGet("{roleId}")]
        public async Task<ActionResult<RoleDto>> GetRoleById(string roleId)
        {
            var role = await _serviceManager.RoleService.GetRoleByIdAsync(roleId);
            if (role == null)
                return NotFound(new { message = $"Role with ID '{roleId}' not found." });

            return Ok(role);
        }

        /// <summary>
        /// Get role by name
        /// </summary>
        [HttpGet("by-name/{roleName}")]
        public async Task<ActionResult<RoleDto>> GetRoleByName(string roleName)
        {
            var role = await _serviceManager.RoleService.GetRoleByNameAsync(roleName);
            if (role == null)
                return NotFound(new { message = $"Role '{roleName}' not found." });

            return Ok(role);
        }

        /// <summary>
        /// Create a new role
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<RoleDto>> CreateRole([FromBody] CreateRoleDto createRoleDto)
        {
            var result = await _serviceManager.RoleService.CreateRoleAsync(createRoleDto);
            
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

            var createdRole = await _serviceManager.RoleService.GetRoleByNameAsync(createRoleDto.RoleName);
            return CreatedAtAction(nameof(GetRoleById), new { roleId = createdRole!.Id }, createdRole);
        }

        /// <summary>
        /// Update an existing role
        /// </summary>
        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateRole(string roleId, [FromBody] UpdateRoleDto updateRoleDto)
        {
            var result = await _serviceManager.RoleService.UpdateRoleAsync(roleId, updateRoleDto);
            
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

            return NoContent();
        }

        /// <summary>
        /// Delete a role
        /// </summary>
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var result = await _serviceManager.RoleService.DeleteRoleAsync(roleId);
            
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

            return NoContent();
        }

        /// <summary>
        /// Assign role to user by email
        /// </summary>
        [HttpPost("assign-by-email")]
        public async Task<IActionResult> AssignRoleByEmail([FromBody] AssignRoleByEmailDto dto)
        {
            var result = await _serviceManager.RoleService.AssignRoleByEmailAsync(dto);
            
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

            return Ok(new { message = $"Role '{dto.RoleName}' assigned successfully to user with email '{dto.Email}'." });
        }

        /// <summary>
        /// Assign role to user by username
        /// </summary>
        [HttpPost("assign-by-username")]
        public async Task<IActionResult> AssignRoleByUsername([FromBody] AssignRoleByUsernameDto dto)
        {
            var result = await _serviceManager.RoleService.AssignRoleByUsernameAsync(dto);
            
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

            return Ok(new { message = $"Role '{dto.RoleName}' assigned successfully to user with username '{dto.Username}'." });
        }

        /// <summary>
        /// Assign role to user by academic code
        /// </summary>
        [HttpPost("assign-by-academic-code")]
        public async Task<IActionResult> AssignRoleByAcademicCode([FromBody] AssignRoleByAcademicCodeDto dto)
        {
            var result = await _serviceManager.RoleService.AssignRoleByAcademicCodeAsync(dto);
            
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

            return Ok(new { message = $"Role '{dto.RoleName}' assigned successfully to user with academic code '{dto.AcademicCode}'." });
        }

        /// <summary>
        /// Get all roles for a specific user
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetUserRoles(string userId)
        {
            try
            {
                var roles = await _serviceManager.RoleService.GetUserRolesAsync(userId);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
