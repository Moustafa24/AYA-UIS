using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class SemesterRepository : GenericRepository<Semester, int>, ISemesterRepository
    {
        public SemesterRepository(AYA_UIS_InfoDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Semester>> GetByStudyYearIdAsync(int studyYearId)
        {
            return await _dbContext.Semesters
                .Where(s => s.StudyYearId == studyYearId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}