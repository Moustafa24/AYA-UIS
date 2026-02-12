using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface IRegistrationRepository : IGenericRepository<Registration, int>
    {
        Task<IEnumerable<Registration>> GetByUserIdAsync(string userId);
        Task<IEnumerable<Registration>> GetByCourseIdAsync(int courseId);
    }
}
