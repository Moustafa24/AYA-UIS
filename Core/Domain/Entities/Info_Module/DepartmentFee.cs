using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Info_Module
{
    public class DepartmentFee  : BaseEntities<int>
    {

        public int DepartmentId { get; set; }
        public Department Department { get; set; } 

        public int GradeYearId { get; set; }
        public GradeYear GradeYear { get; set; }
        public decimal FeeAmount { get; set; }
    }
}
