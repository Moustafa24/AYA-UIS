using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface ICourseRepository : IGenericRepository<Course, int>
    {
        public Task<Course?> GetCourseUplaodsAsync(int id);
        public Task<IEnumerable<Course>> GetDepartmentCoursesAsync(int departmentId);
        public Task<IEnumerable<Course>> GetCourseDependenciesAsync(int courseId);
        public Task<IEnumerable<Course>> GetCoursePrerequisitesAsync(int courseId);
        public Task<IEnumerable<Course>> GetPassedCoursesByUserAsync(string userId);
    }
}
