using AutoMapper;
using Domain.Contracts;
using AYA_UIS.Shared.Exceptions;
using AYA_UIS.Core.Abstractions.Contracts;
using Shared.Dtos.Info_Module;
using AYA_UIS.Core.Domain.Entities.Models;

namespace AYA_UIS.Core.Services.Implementations
{
    public class DepartmentFeeService : IDepartmentFeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentFeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<IEnumerable<DepartmentFeeDtos>> GetAllDepartmentFeeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentFeeDtos?> GetDepartmentFeeByCompositeKeyAsync(string departmentName, string gradeYear)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateByCompositeKeyAsync(string departmentName, string gradeYear, DepartmentFeeDtos dto)
        {
            throw new NotImplementedException();
        }

        // // Get all fees
        // public async Task<IEnumerable<DepartmentFeeDto>> GetAllDepartmentFeeAsync()
        // {
        //     var departmentFees = await _unitOfWork.GetRepository<DepartmentFee, int>().GetAllAsync();
        //     return _mapper.Map<IEnumerable<DepartmentFeeDto>>(departmentFees);
        // }



    }
}
