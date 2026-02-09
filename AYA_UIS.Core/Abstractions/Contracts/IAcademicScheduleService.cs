using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shared.Dtos.Info_Module;
using Shared.Dtos.Info_Module.AcademicSheduleDtos;

namespace AYA_UIS.Core.Abstractions.Contracts
{
    public interface IAcademicSchedulesService
    {
        Task<IEnumerable<AcademicSchedulesDto>> GetAllAsync();
        Task<AcademicSchedulesDto?> GetByNameAsync(string nameScadules);

        // Add مع رفع صورة اختياري
        Task<int> AddAsync(string title, string description, string fileId, string fileUrl, string uploadedByUserId);

        // Delete مع حذف الصورة
        Task<bool> DeleteByNameAsync(int id);
    }
}
