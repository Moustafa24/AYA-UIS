using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface IUserStudyYearRepository : IGenericRepository<UserStudyYear, int>
    {
        Task<IEnumerable<UserStudyYear>> GetByUserIdAsync(string userId);
        Task<UserStudyYear?> GetCurrentByUserIdAsync(string userId);
        Task<UserStudyYear?> GetByUserAndStudyYearAsync(string userId, int studyYearId);
        Task<IEnumerable<UserStudyYear>> GetByStudyYearIdAsync(int studyYearId);
    }
}
