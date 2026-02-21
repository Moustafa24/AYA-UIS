using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence;

namespace Presistence.Repositories
{
    public class AcademicScheduleRepository : GenericRepository<AcademicSchedule, int>, IAcademicScheduleRepository
    {
        public AcademicScheduleRepository(UniversityDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<AcademicSchedule?> GetByTitleAsync(string title)
        {
            return await _dbContext.AcademicSchedules
                .FirstOrDefaultAsync(a => a.Title.Contains(title));
        }

        public async Task<IEnumerable<AcademicSchedule>> GetAllWithDetailsAsync()
        {
            return await _dbContext.AcademicSchedules
                .Include(a => a.Department)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AcademicSchedule?> UploadSemesterAcademicScheduleAsync(AcademicSchedule schedule)
        {
            await _dbContext.AcademicSchedules.AddAsync(schedule);
            return schedule;
        }
    }
}
