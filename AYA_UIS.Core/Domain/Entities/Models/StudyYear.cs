using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Entities.Identity;
using AYA_UIS.Core.Domain.Entities.Models;

namespace AYA_UIS.Core.Domain.Entities.Models
{
    public class StudyYear : BaseEntities<int>
    {
        public int Year { get; set; } // e.g., 2024, 2025, etc.
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public ICollection<DepartmentFee> DepartmentFees { get; set; } = new List<DepartmentFee>();
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}