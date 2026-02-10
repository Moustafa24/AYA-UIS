using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface ICourseRepository : IGenericRepository<Course, int>
    {
        public Task<Course?> GetCourseUplaodsAsync(int id);
        public Task<Course?> GetYearCourseRegistrationAsync(int id, int yearId);
    }
}
