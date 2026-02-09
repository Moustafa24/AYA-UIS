using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class CourseRepository : GenericRepository<Course, int>, ICourseRepository
    {
        public CourseRepository(AYA_UIS_InfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Course>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _dbContext.Courses
                .Where(c => c.DepartmentId == departmentId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Course?> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.Courses
                .Include(c => c.Department)
                .Include(c => c.Registrations)
                .Include(c => c.CourseUploads)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
