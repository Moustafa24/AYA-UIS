using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface ISemesterRepository : IGenericRepository<Semester, int>
    {
        public Task<IEnumerable<Semester>> GetByStudyYearIdAsync(int studyYearId);
        Task<Semester?> GetActiveSemesterByStudyYearIdAsync(int studyYearId);
        Task<bool> IsActiveSemesterAsync(int semesterId);
        Task<bool> IsSemesterBelongsToStudyYearAsync(int semesterId, int studyYearId);
    }
}