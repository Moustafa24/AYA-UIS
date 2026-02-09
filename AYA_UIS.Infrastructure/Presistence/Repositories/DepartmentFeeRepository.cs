using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class DepartmentFeeRepository : GenericRepository<DepartmentFee, int>, IDepartmentFeeRepository
    {
        public DepartmentFeeRepository(AYA_UIS_InfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<DepartmentFee?> GetByCompositeKeyAsync(string departmentName, int studyYear)
        {
            return await _dbContext.DepartmentFees
                .Include(df => df.Department)
                .Include(df => df.StudyYear)
                .Include(df => df.Fees)
                .FirstOrDefaultAsync(df => df.Department.Name == departmentName
                    && df.StudyYear.Year == studyYear);
        }

        public async Task<IEnumerable<DepartmentFee>> GetAllWithDetailsAsync()
        {
            return await _dbContext.DepartmentFees
                .Include(df => df.Department)
                .Include(df => df.StudyYear)
                .Include(df => df.Fees)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
