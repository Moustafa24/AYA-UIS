using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class DepartmentRepository : GenericRepository<Department, int>, IDepartmentRepository
    {
        public DepartmentRepository(AYA_UIS_InfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Department?> GetByNameAsync(string name)
        {
            return await _dbContext.Departments
                .FirstOrDefaultAsync(d => d.Name == name);
        }

        public async Task<Department?> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.Departments
                .Include(d => d.Courses)
                .Include(d => d.StudyYears)
                .Include(d => d.Fees)
                .Include(d => d.AcademicSchedules)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Department>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Departments
                .Include(d => d.Courses)
                .Include(d => d.StudyYears)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}