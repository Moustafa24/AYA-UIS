using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Auth_Module
{
    public class UpdateRoleDto
    {
        public string Email { get; set; } = string.Empty;
        public string NewRole { get; set; } = string.Empty ;
    }
}
