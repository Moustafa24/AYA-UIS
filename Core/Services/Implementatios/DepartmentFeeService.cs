using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Info_Module;
using Domain.Exceptions;
using Presistence.Specifications;
using Services.Abstraction.Contracts;
using Shared.Dtos.Info_Module;

namespace Services.Implementations
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

        // Get all fees
        public async Task<IEnumerable<DepartmentFeeDtos>> GetAllDepartmentFeeAsync()
        {
            var repo = _unitOfWork.GetRepository<DepartmentFee, int>();

            //  Specification Include 
            var spec = new DepartmentFeeWithIncludesSpec();

            var data = await repo.ListAsync(spec); 
            return data == null ? throw new DepartmentFeeNotFoundException() : _mapper.Map<IEnumerable<DepartmentFeeDtos>>(data);
        }

        // Get fee by Department + GradeYear
        public async Task<DepartmentFeeDtos?> GetDepartmentFeeByCompositeKeyAsync(string departmentName, string gradeYear)
        {
            var repo = _unitOfWork.GetRepository<DepartmentFee, int>();
            var spec = new DepartmentFeeWithIncludesSpec(departmentName, gradeYear);
            var entity = await repo.GetAsync(spec);
            return entity == null ? throw new DepartmentFeeNotFoundException() : _mapper.Map<DepartmentFeeDtos>(entity);
        }

        // Update FeeAmount only
        public async Task<bool> UpdateByCompositeKeyAsync(string departmentName, string gradeYear, DepartmentFeeDtos dto)
        {
            var repo = _unitOfWork.GetRepository<DepartmentFee, int>();
            var spec = new DepartmentFeeWithIncludesSpec(departmentName, gradeYear);
            var entity = await repo.GetAsync(spec);

            if (entity == null)
                throw new DepartmentFeeNotFoundException();

            entity.FeeAmount = dto.FeeAmount;
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

    }
}
