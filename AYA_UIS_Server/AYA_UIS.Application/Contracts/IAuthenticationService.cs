using Shared.Dtos.Auth_Module;

namespace AYA_UIS.Application.Contracts
{
    public interface IAuthenticationService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        Task<string> ResetPasswordAsync(string email, string newPassword);
        Task<string> UpdateRoleByEmailAsync(string email, string newRole);
    }
}
