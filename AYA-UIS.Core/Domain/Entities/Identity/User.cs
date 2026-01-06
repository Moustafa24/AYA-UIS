using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Academic_Code { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty ;

    }
}
