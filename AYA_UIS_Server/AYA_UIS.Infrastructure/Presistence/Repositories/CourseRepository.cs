using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class CourseRepository : GenericRepository<Course, int>, ICourseRepository
    {
        public CourseRepository(AYA_UIS_InfoDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<Course?> GetCourseUplaodsAsync(int id)
        {
            return await _dbContext.Courses
                .Include(c => c.CourseUpload)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course?> GetYearCourseRegistrationAsync(int id, int yearId)
        {
            return await _dbContext.Courses
                .Include(c => c.Registrations.Where(r => r.StudyYearId == yearId))
                    .ThenInclude(r => r.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }


    }
}
