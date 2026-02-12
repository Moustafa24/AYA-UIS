using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Entities.Models;

namespace AYA_UIS.Core.Domain.Entities.Models
{
    public class Fee : BaseEntities<int>
    {

        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty; // Tuition, Lab, Registration, etc.
        public int DepartmentFeeId { get; set; }
        public DepartmentFee DepartmentFee { get; set; } = null!;
    }
}