using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace AYA_UIS.Application.Commands.DepartmentFees
{
    public record CreateDepartmentFeeCommand : IRequest<int>
    {
        public int DepartmentId { get; set; }
        public int StudyYearId { get; set; }
    }
   
}