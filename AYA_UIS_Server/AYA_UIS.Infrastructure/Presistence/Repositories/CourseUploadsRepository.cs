using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence;

namespace Presistence.Repositories
{
    public class CourseUploadsRepository : GenericRepository<CourseUpload, int>, ICourseUploadsRepository
    {
        public CourseUploadsRepository(UniversityDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<CourseUpload>> GetByCourseIdAsync(int courseId)
        {
            return await _dbContext.CourseUploads
                .Where(cu => cu.CourseId == courseId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseUpload>> GetByUserIdAsync(string userId)
        {
            return await _dbContext.CourseUploads
                .Where(cu => cu.UploadedByUserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
