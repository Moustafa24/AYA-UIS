using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface ISemesterRepository : IGenericRepository<Semester, int>
    {
        public Task<IEnumerable<Semester>> GetByStudyYearIdAsync(int studyYearId);
    }
}