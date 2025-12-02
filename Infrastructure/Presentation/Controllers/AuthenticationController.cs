using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Services.Abstraction.Contracts;
using Shared.Dtos.Auth_Module;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController (IServiceManager _serviceManager):ControllerBase
    {

        // Post =>  Register 
        [HttpPost("Register")]
        [EnableRateLimiting("PolicyLimitRate")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResultDto>> RegisterAsync(RegisterDto registerDto)
        => await _serviceManager.AuthenticationService.RegisterAsync(registerDto);

        // Post = >  Login 
        [EnableRateLimiting("PolicyLimitRate")]
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> LoginAsync(LoginDto loginDto)
            => await _serviceManager.AuthenticationService.LoginAsync(loginDto);


        [HttpPut("reset-password")]
        [EnableRateLimiting("PolicyLimitRate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetPasswordByAdmin(ResetPasswordDto resetPasswordDto)
        => Ok(await _serviceManager.AuthenticationService.ResetPasswordAsync(resetPasswordDto.Email , resetPasswordDto.NewPassword));

        [HttpPut("update-role")]
        [EnableRateLimiting("PolicyLimitRate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoleByEmail(UpdateRoleDto updateRoleDto)
        => Ok(await _serviceManager.AuthenticationService.UpdateRoleByEmailAsync(updateRoleDto.Email, updateRoleDto.NewRole));
        


    }
}
