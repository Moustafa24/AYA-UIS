using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class RegistrationRepository : GenericRepository<Registration, int>, IRegistrationRepository
    {
        public RegistrationRepository(AYA_UIS_InfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Registration>> GetByUserIdAsync(string userId)
        {
            return await _dbContext.Registrations
                .Where(r => r.UserId == userId)
                .Include(r => r.Course)
                .Include(r => r.StudyYear)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Registration>> GetByCourseIdAsync(int courseId)
        {
            return await _dbContext.Registrations
                .Where(r => r.CourseId == courseId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
