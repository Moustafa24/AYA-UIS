using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Dtos.Info_Module.UserDtos
{
    public class UpdateStudentAllowedCreditsDto
    {
        public string? AcademicCode { get; set; }
        public int AllowedCredits { get; set; }
    }
}