using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Dtos.Info_Module.UserDtos
{
    public class UpdateStudentTotalGPADto
    {
        public string? AcademicCode { get; set; }
        public float TotalGPA { get; set; }
    }
}