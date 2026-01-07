using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Contracts
{
    public interface IServiceManager
    {
        public IDepartmentFeeService DepartmentFeeService { get; }
        public IAcademicSchedulesService  AcademicSchedules { get; }

        public IAuthenticationService AuthenticationService { get; }
        public IRoleService RoleService { get; }
    }
}
