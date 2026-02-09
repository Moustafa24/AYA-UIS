using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Dtos.Info_Module.DepartmentDtos;

namespace Abstractions.Contracts
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task<DepartmentDetailsDto?> GetByIdAsync(int id);
        Task<int> AddAsync(DepartmentDto department);
        Task<int> UpdateAsync(int id, DepartmentDto department);
        Task<bool> DeleteByIdAsync(int id); 
    }
}