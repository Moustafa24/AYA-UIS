using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface IDepartmentRepository : IGenericRepository<Department, int>
    {
        Task<Department?> GetByNameAsync(string name);
        Task<Department?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Department>> GetAllWithDetailsAsync();
    }
}
