using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Dtos.Info_Module.UserDtos
{
    public class UpdateStudentTotalCreditsDto
    {
        public string? AcademicCode { get; set; } // null if 
        public int TotalCredits { get; set; }
    }
}