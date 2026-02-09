using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface IFeeRepository : IGenericRepository<Fee, int>
    {
        Task<IEnumerable<Fee>> GetByDepartmentFeeIdAsync(int departmentFeeId);
    }
}
