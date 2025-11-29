using System;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstraction.Contracts;
using Services.Implementatios;
using Shared.Common;

namespace Services.Implementations
{
    public class ServiceManager(IUnitOfWork _unitOfWork,
    IMapper _mapper,
    IHttpContextAccessor _httpContextAccessor,
    IConfiguration _configurationn,
    UserManager<User> _userManager,                 // ✔ صح
    IOptions<JwtOptions> _options,
     RoleManager<IdentityRole> _roleManager          // ✔ صح
        ) : IServiceManager
    {
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;
        //private readonly Lazy<IDepartmentFeeService> _departmentFeeService;
        //private readonly Lazy<IAcademicSchedulesService> _academicSchedules;
        //private readonly IHttpContextAccessor  _httpContextAccessor;
        //private readonly IConfiguration _configurationn;

        //public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        //{
        //    _unitOfWork = unitOfWork;
        //    _mapper = mapper;
        //    _departmentFeeService = new Lazy<IDepartmentFeeService>(() => new DepartmentFeeService(_unitOfWork, _mapper));
        //    _academicSchedules = new Lazy<IAcademicSchedulesService>(() => new AcademicSchedulesService(_unitOfWork, _mapper  , _httpContextAccessor , _configurationn));
        //}

        private readonly Lazy<IDepartmentFeeService> _departmentFeeService = new Lazy<IDepartmentFeeService>(() => new DepartmentFeeService(_unitOfWork, _mapper));
        private readonly Lazy<IAcademicSchedulesService> _academicSchedules = new Lazy<IAcademicSchedulesService>(() => new AcademicSchedulesService(_unitOfWork, _mapper, _httpContextAccessor, _configurationn));
        private readonly Lazy<IAuthenticationService> _authService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _options,  _roleManager));
        public IDepartmentFeeService DepartmentFeeService => _departmentFeeService.Value;
        public IAcademicSchedulesService AcademicSchedules => _academicSchedules.Value;

        public IAuthenticationService AuthenticationService => _authService.Value;
    }
}
