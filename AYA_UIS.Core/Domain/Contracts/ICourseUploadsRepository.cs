using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface ICourseUploadsRepository : IGenericRepository<CourseUploads, int>
    {
        Task<IEnumerable<CourseUploads>> GetByCourseIdAsync(int courseId);
        Task<IEnumerable<CourseUploads>> GetByUserIdAsync(string userId);
    }
}
