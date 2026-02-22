using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence;

namespace Presistence.Repositories
{
    public class SemesterRepository : GenericRepository<Semester, int>, ISemesterRepository
    {
        public SemesterRepository(UniversityDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Semester>> GetByStudyYearIdAsync(int studyYearId)
        {
            return await _dbContext.Semesters
                .Where(s => s.StudyYearId == studyYearId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Semester?> GetActiveSemesterByStudyYearIdAsync(int studyYearId)
        {
            return await _dbContext.Semesters
                .Where(s => s.StudyYearId == studyYearId && s.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsActiveSemesterAsync(int semesterId)
        {
            return await _dbContext.Semesters
                .AnyAsync(s => s.Id == semesterId && s.IsActive);
        }

        public async Task<bool> IsSemesterBelongsToStudyYearAsync(int semesterId, int studyYearId)
        {
            return await _dbContext.Semesters
                .AnyAsync(s => s.Id == semesterId && s.StudyYearId == studyYearId);
        }
    }
}