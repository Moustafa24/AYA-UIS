using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class StudyYearRepository : GenericRepository<StudyYear, int>, IStudyYearRepository
    {
        public StudyYearRepository(AYA_UIS_InfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<StudyYear>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _dbContext.StudyYears
                .Where(sy => sy.DepartmentId == departmentId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<StudyYear?> GetCurrentStudyYearAsync()
        {
            return await _dbContext.StudyYears
                .Where(sy => sy.IsCurrent)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
