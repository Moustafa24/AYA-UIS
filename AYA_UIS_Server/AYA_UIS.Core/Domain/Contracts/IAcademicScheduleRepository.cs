using AYA_UIS.Core.Domain.Entities.Models;

namespace Domain.Contracts
{
    public interface IAcademicScheduleRepository : IGenericRepository<AcademicSchedule, int>
    {
        Task<AcademicSchedule?> GetByTitleAsync(string title);
        Task<IEnumerable<AcademicSchedule>> GetAllWithDetailsAsync();
        Task<AcademicSchedule?> UploadSemesterAcademicScheduleAsync(AcademicSchedule schedule);
    }
}
