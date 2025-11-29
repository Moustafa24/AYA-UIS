using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Auth_Module;

namespace Services.Abstraction.Contracts
{
    public interface IAuthenticationService
    {

        Task<UserResultDto> LoginAsync(LoginDto loginDto);

        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);

     
        Task<string> ResetPasswordAsync(string email, string newPassword);


        Task<string> UpdateRoleByEmailAsync(string email, string newRole);


    }
}
