using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Info_Module
{
    public  record DepartmentFeeDtos
    {
        
      public  int Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string GradeYear { get; set; } = string.Empty ;
        public decimal FeeAmount { get; set; } 
    }
}
