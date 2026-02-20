using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYA_UIS.Core.Abstractions.Contracts
{
    public interface IServiceManager
    {
        public IAcademicSchedulesService  AcademicSchedules { get; }

        public IAuthenticationService AuthenticationService { get; }
        public IRoleService RoleService { get; }
    }
}
