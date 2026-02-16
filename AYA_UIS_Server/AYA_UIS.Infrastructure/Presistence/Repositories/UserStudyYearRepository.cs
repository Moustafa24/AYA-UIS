using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class UserStudyYearRepository : GenericRepository<UserStudyYear, int>, IUserStudyYearRepository
    {
        public UserStudyYearRepository(AYA_UIS_InfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<UserStudyYear>> GetByUserIdAsync(string userId)
        {
            return await _dbContext.UserStudyYears
                .Include(usy => usy.StudyYear)
                    .ThenInclude(sy => sy.Department)
                .Where(usy => usy.UserId == userId)
                .OrderBy(usy => usy.StudyYear.StartYear)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<UserStudyYear?> GetCurrentByUserIdAsync(string userId)
        {
            return await _dbContext.UserStudyYears
                .Include(usy => usy.StudyYear)
                    .ThenInclude(sy => sy.Department)
                .Where(usy => usy.UserId == userId && usy.IsCurrent)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<UserStudyYear?> GetByUserAndStudyYearAsync(string userId, int studyYearId)
        {
            return await _dbContext.UserStudyYears
                .Include(usy => usy.StudyYear)
                .Where(usy => usy.UserId == userId && usy.StudyYearId == studyYearId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserStudyYear>> GetByStudyYearIdAsync(int studyYearId)
        {
            return await _dbContext.UserStudyYears
                .Include(usy => usy.StudyYear)
                .Where(usy => usy.StudyYearId == studyYearId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
