using System;
using AutoMapper;
using Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Services.Abstraction.Contracts;
using Services.Implementatios;

namespace Services.Implementations
{
    public class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Lazy<IDepartmentFeeService> _departmentFeeService;
        private readonly Lazy<IAcademicSchedulesService> _academicSchedules;
        private readonly IHttpContextAccessor  _httpContextAccessor;
        private readonly IConfiguration _configurationn;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _departmentFeeService = new Lazy<IDepartmentFeeService>(() => new DepartmentFeeService(_unitOfWork, _mapper));
            _academicSchedules = new Lazy<IAcademicSchedulesService>(() => new AcademicSchedulesService(_unitOfWork, _mapper  , _httpContextAccessor , _configurationn));
        }

        public IDepartmentFeeService DepartmentFeeService => _departmentFeeService.Value;
        public IAcademicSchedulesService AcademicSchedules => _academicSchedules.Value;
    }
}
