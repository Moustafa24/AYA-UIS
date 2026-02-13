using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class SemesterRepository : GenericRepository<Semester, int>, ISemesterRepository
    {
        public SemesterRepository(AYA_UIS_InfoDbContext dbContext) : base(dbContext)
        {
        }
    }
}