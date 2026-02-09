using System;
using AutoMapper;
using AYA_UIS.Core.Abstractions.Contracts;
using Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AYA_UIS.Core.Services.Implementations;
using Shared.Common;
using AYA_UIS.Core.Services.Implementations;
using AYA_UIS.Core.Domain.Entities.Identity;

namespace AYA_UIS.Core.Services.Implementations
{
    public class ServiceManager(IUnitOfWork _unitOfWork,
    IMapper _mapper,
    IHttpContextAccessor _httpContextAccessor,
    IConfiguration _configurationn,
    UserManager<User> _userManager,             
    IOptions<JwtOptions> _options,
     RoleManager<IdentityRole> _roleManager ,
      ILogger<AuthenticationService> _logger
        ) : IServiceManager
    {

        private readonly Lazy<IDepartmentFeeService> _departmentFeeService = new Lazy<IDepartmentFeeService>(() => new DepartmentFeeService(_unitOfWork, _mapper));
        private readonly Lazy<IAcademicSchedulesService> _academicSchedules = new Lazy<IAcademicSchedulesService>(() => new AcademicScheduleService(_unitOfWork, _mapper));
        private readonly Lazy<IAuthenticationService> _authService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _options,  _roleManager , _logger));
        private readonly Lazy<IRoleService> _roleService = new Lazy<IRoleService>(() => new RoleService(_roleManager, _userManager));
        
        public IDepartmentFeeService DepartmentFeeService => _departmentFeeService.Value;
        public IAcademicSchedulesService AcademicSchedules => _academicSchedules.Value;
        public IAuthenticationService AuthenticationService => _authService.Value;
        public IRoleService RoleService => _roleService.Value;
    }
}
