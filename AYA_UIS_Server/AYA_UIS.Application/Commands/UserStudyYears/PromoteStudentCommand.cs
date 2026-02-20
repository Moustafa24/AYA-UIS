using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace AYA_UIS.Application.Commands.UserStudyYears
{
    public class PromoteStudentCommand : IRequest<Unit>
    {
        public string AcademicCode { get; set; }

        public PromoteStudentCommand(string academicCode)
        {
            AcademicCode = academicCode;
        }
    }
}