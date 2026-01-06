using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shared.Dtos.Info_Module;

namespace Services.Abstraction.Contracts
{
    public interface IAcademicSchedulesService
    {
        Task<IEnumerable<AcademicSchedulesDtos>> GetAllAsync();
        Task<AcademicSchedulesDtos?> GetByNameAsync(string nameScadules);

        // Add مع رفع صورة اختياري
        Task<AcademicSchedulesDtos> AddAsync( string nameScadules , IFormFile file );

        // Delete مع حذف الصورة
        Task<bool> DeleteByNameAsync(string nameScadules);
    }
}
