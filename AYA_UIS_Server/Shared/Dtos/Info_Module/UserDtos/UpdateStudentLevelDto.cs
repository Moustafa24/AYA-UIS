using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Enums;

namespace Shared.Dtos.Info_Module.UserDtos
{
    public class UpdateStudentLevelDto
    {
        public string? AcademicCode { get; set; }
        public Levels Level { get; set; }
    }
}