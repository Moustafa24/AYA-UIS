using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface ICourseUploadsRepository : IGenericRepository<CourseUpload, int>
    {
        Task<IEnumerable<CourseUpload>> GetByCourseIdAsync(int courseId);
        Task<IEnumerable<CourseUpload>> GetByUserIdAsync(string userId);
    }
}
