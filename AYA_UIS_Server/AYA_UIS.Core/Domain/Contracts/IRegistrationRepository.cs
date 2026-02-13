using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface IRegistrationRepository : IGenericRepository<Registration, int>
    {
        Task<IEnumerable<Registration>> GetByUserIdAsync(string userId);
        Task<IEnumerable<Registration>> GetByCourseIdAsync(int courseId);
        Task<Registration?> GetByUserAndCourseAsync(string userId, int courseId, int studyYearId);
        Task<IEnumerable<Registration>> GetByUserAndStudyYearAsync(string userId, int studyYearId);
        Task<IEnumerable<Registration>> GetByUserAsync(string userId, int? studyYearId = null);
        Task<IEnumerable<Registration>> GetAllAsync(int? courseId = null, int? studyYearId = null, int? semesterId = null, string? userId = null);
    }
}
