using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shared.Dtos.Info_Module.AcademicSheduleDtos
{
    public class CreateSemesterAcademicScheduleDto
    {
        public string Title { get; set; } = string.Empty;
        public IFormFile File { get; set; } = null!;
        public string? Description { get; set; }
    }
}