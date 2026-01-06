using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AcademicSchedulesNotFoundException : NotFoundException
    {
        public AcademicSchedulesNotFoundException(string name) : base($"AcademicSchedules {name} Not Found Or Unavailable") 
        {
            
        }
    }
}
