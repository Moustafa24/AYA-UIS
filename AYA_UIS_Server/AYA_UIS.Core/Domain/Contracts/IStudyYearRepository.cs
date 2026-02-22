using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface IStudyYearRepository : IGenericRepository<StudyYear, int>
    {
        Task<StudyYear?> GetCurrentStudyYearAsync();
        Task<bool> IsCurrentStudyYearAsync(int studyYearId);
    }
}
