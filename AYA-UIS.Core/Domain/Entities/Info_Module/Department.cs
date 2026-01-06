using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Info_Module
{
    public class Department : BaseEntities<int>
    {
       
        public string Name { get; set; } = string.Empty;

        public ICollection<DepartmentFee> Fees { get; set; } = [];
    }
}
