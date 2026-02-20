using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class FeeRepository : GenericRepository<Fee, int>, IFeeRepository
    {
        public FeeRepository(AYA_UIS_InfoDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<IEnumerable<Fee>> GetFeesOfDepartmentForStudyYear(int departmentId, int studyYearId)
        {
            return await _dbContext.Fees
                .Where(f => f.DepartmentId == departmentId && f.StudyYearId == studyYearId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Fee>> GetFeesOfStudyYear(int studyYearId)
        {
            return await _dbContext.Fees
                .Where(f => f.StudyYearId == studyYearId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
