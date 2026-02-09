using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface IDepartmentFeeRepository : IGenericRepository<DepartmentFee, int>
    {
        Task<DepartmentFee?> GetByCompositeKeyAsync(string departmentName, int studyYear);
        Task<IEnumerable<DepartmentFee>> GetAllWithDetailsAsync();
    }
}
