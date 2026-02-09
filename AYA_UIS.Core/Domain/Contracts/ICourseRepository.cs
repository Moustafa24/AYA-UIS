using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface ICourseRepository : IGenericRepository<Course, int>
    {
        Task<IEnumerable<Course>> GetByDepartmentIdAsync(int departmentId);
        Task<Course?> GetByIdWithDetailsAsync(int id);
    }
}
