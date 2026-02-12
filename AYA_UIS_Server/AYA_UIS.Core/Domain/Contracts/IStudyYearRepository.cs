using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface IStudyYearRepository : IGenericRepository<StudyYear, int>
    {
        Task<IEnumerable<StudyYear>> GetByDepartmentIdAsync(int departmentId);
    }
}
