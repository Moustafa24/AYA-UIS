using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Info_Module
{
    public class AcademicSchedulesDtos
    {
        
        public int? Id { get; set; }
        public string NameScadules { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty ;
        public string? Url { get; set; }
        public string? Description { get; set; }

    }
}
