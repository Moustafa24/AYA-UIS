using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Info_Module;

namespace Services.Abstraction.Contracts
{
    public interface IDepartmentFeeService
    {
        Task<IEnumerable<DepartmentFeeDtos>> GetAllDepartmentFeeAsync();
        Task<DepartmentFeeDtos?> GetDepartmentFeeByCompositeKeyAsync(string departmentName, string gradeYear);
        Task<bool> UpdateByCompositeKeyAsync(string departmentName, string gradeYear, DepartmentFeeDtos dto);

    }
}
