using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Dtos.Info_Module.StudyYearDtos
{
    public class CreateStudyYearDto
    {
        public int StartYear { get; set; } // e.g., 2024, 2025, etc.
        public int EndYear { get; set; } // e.g., 2025, 
    }
}