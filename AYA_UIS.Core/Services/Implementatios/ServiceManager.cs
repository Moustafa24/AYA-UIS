using System;
using AYA_UIS.Application.Contracts;
using AYA_UIS.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Common;

namespace AYA_UIS.Core.Services.Implementations
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthenticationService> _authService;
        private readonly Lazy<IRoleService> _roleService;

        public ServiceManager(
            UserManager<User> userManager,
            IOptions<JwtOptions> options,
            RoleManager<IdentityRole> roleManager,
            ILogger<AuthenticationService> logger)
        {
            _authService = new Lazy<IAuthenticationService>(
                () => new AuthenticationService(userManager, options, roleManager, logger));
            _roleService = new Lazy<IRoleService>(
                () => new RoleService(roleManager, userManager));
        }

        public IAuthenticationService AuthenticationService => _authService.Value;
        public IRoleService RoleService => _roleService.Value;
    }
}
