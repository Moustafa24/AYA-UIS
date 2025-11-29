using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Auth_Module
{
    public record UserResultDto(string DisplayName, string Token, string Email , string role ); 
    
}
