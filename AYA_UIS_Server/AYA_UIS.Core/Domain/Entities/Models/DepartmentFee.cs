using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Entities.Models;
using Microsoft.VisualBasic;

namespace AYA_UIS.Core.Domain.Entities.Models
{
    public class DepartmentFee: BaseEntities<int>
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public int StudyYearId { get; set; }
        public StudyYear StudyYear { get; set; } = null!;
        public ICollection<Fee> Fees { get; set; } = new List<Fee>();
    }
}
