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

        public async Task<IEnumerable<Fee>> GetByDepartmentFeeIdAsync(int departmentFeeId)
        {
            return await _dbContext.Fees
                .Where(f => f.DepartmentFeeId == departmentFeeId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
