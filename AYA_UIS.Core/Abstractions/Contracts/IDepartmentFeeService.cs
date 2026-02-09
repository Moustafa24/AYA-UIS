using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Info_Module;
using Shared.Dtos.Info_Module.DepartmentFeeDtos;

namespace AYA_UIS.Core.Abstractions.Contracts
{
    public interface IDepartmentFeeService
    {
        Task<IEnumerable<DepartmentFeeDto>> GetAllDepartmentFeeAsync();
        Task<DepartmentFeeDto?> GetDepartmentFeeByCompositeKeyAsync(string departmentName, string gradeYear);
        Task<bool> UpdateByCompositeKeyAsync(string departmentName, string gradeYear, DepartmentFeeDto dto);

    }
}
