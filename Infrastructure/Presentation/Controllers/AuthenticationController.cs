using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<UserResultDto>> RegisterAsync(RegisterDto registerDto)
        => await _serviceManager.AuthenticationService.RegisterAsync(registerDto);

        // Post = >  Login 

        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> LoginAsync(LoginDto loginDto)
            => await _serviceManager.AuthenticationService.LoginAsync(loginDto);

        [HttpPut("reset-password")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetPasswordByAdmin(string email, string newPassword)
        => Ok(await _serviceManager.AuthenticationService.ResetPasswordAsync(email, newPassword));


        [HttpPut("update-role")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> UpdateRoleByEmail(string email, string newRole)
        => Ok(await _serviceManager.AuthenticationService.UpdateRoleByEmailAsync(email, newRole));
        


    }
}
