using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence;

namespace Presistence.Repositories
{
    public class StudyYearRepository : GenericRepository<StudyYear, int>, IStudyYearRepository
    {
        public StudyYearRepository(UniversityDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<StudyYear?> GetCurrentStudyYearAsync()
        {
            return await _dbContext.StudyYears
                .Where(sy => sy.IsCurrent)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsCurrentStudyYearAsync(int studyYearId)
        {
            return await _dbContext.StudyYears
                .AnyAsync(sy => sy.Id == studyYearId && sy.IsCurrent);
        }
    }
}
