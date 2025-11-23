using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DepartmentFeeNotFoundException :NotFoundException
    {
        public DepartmentFeeNotFoundException() :base("Not Found Department Or Grade Year " ) 
        {
        }
    }
}
