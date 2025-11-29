using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction.Contracts;
using Shared.Common;
using Shared.Dtos.Auth_Module;

namespace Services.Implementatios
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IOptions<JwtOptions> _options;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationService(
            UserManager<User> userManager,
            IOptions<JwtOptions> options,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _options = options;
            _roleManager = roleManager;
        }





        // LoginAsync
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {


            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UnAuthoraizedException();

            var validPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!validPassword) throw new UnAuthoraizedException();

            var roles = await _userManager.GetRolesAsync(user);

            return new UserResultDto(
                user.DisplayName,
                await CreateTokenAsync(user),
                user.Email,
                roles.FirstOrDefault(),
                user.Academic_Code,
                user.UserName
            );
        }

        // RegisterAsync
        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var ChekInputValidation = new List<string>();

            if (await _userManager.Users.AnyAsync(u => u.Academic_Code == registerDto.Academic_Code))
                ChekInputValidation.Add("Academic Code already exists.");

            if (await _userManager.Users.AnyAsync(u => u.UserName == registerDto.UserName))
                ChekInputValidation.Add("UserName already exists.");

            if (await _userManager.Users.AnyAsync(u => u.Email == registerDto.Email))
                ChekInputValidation.Add("Email already exists.");

            if (ChekInputValidation.Any())
                throw new ValidationException(ChekInputValidation);

            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,   
                PhoneNumber = registerDto.PhoneNumber,
                Academic_Code = registerDto.Academic_Code
            };



            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }

            
            if (await _roleManager.RoleExistsAsync(registerDto.Role))
                await _userManager.AddToRoleAsync(user, registerDto.Role);

          
            var roles = await _userManager.GetRolesAsync(user);

            var token = await CreateTokenAsync(user);
            return new UserResultDto(
                user.DisplayName,
                token,
                user.Email,
                user.Academic_Code,
                user.UserName,
                roles.FirstOrDefault() 
            );
        }


        // ResetPasswordAsync
        public async Task<string> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new UnAuthoraizedException();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result.Succeeded ?
                "Password Updated Successfully" :
                string.Join(" | ", result.Errors.Select(e => e.Description));
        }


        // UpdateRoleByEmailAsync
        public async Task<string> UpdateRoleByEmailAsync(string email, string newRole)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new UnAuthoraizedException();

            // Remove old roles
            var oldRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, oldRoles);

            // Add new role
            var result = await _userManager.AddToRoleAsync(user, newRole);

            if (!result.Succeeded)
                return "Failed to update role";

            return $"Role updated to {newRole} successfully";
        }


        // Token ==> Encrypted String ==> Return String 

        private async Task<string> CreateTokenAsync(User user)
        {
            var jwtOptions = _options.Value;
            // 1] Create Claims 
            // Name - Email - Roles [M-M]

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , user.DisplayName),
                new Claim (ClaimTypes.Email , user.Email),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            // 2] Secret Key 
            //0a6d8ce9afd9c2791f4028e6308550716cbe69b4d82ab0ae7640bbd76319643a
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

            // 3] Algo 

            var SingInCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4] Generate Token 

            var token = new JwtSecurityToken(issuer: jwtOptions.Issuer, audience: jwtOptions.Audience, claims: claims, expires: DateTime.UtcNow.AddDays(jwtOptions.ExpirationInDay), signingCredentials: SingInCreds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
