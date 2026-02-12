using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Info_Module.FeeDtos;

namespace Shared.Dtos.Info_Module.DepartmentFeeDtos
{
    public  record DepartmentFeeDto
    {
        
      public  int Id { get; set; }
      public string Name { get; set; } = string.Empty;
      public int StartYear { get; set; }
      public int EndYear { get; set; }

      public List<FeeDto> Fees { get; set; } = new List<FeeDto>();
    }
}
