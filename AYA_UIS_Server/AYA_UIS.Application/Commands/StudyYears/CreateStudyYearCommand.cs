using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module.StdudyYearDtos;
using Shared.Dtos.Info_Module.UserStudyYearDtos;

namespace AYA_UIS.Application.Commands.StudyYears
{
    public class CreateStudyYearCommand : IRequest<int>
    {
        public CreateStudyYearDto StudyYearDto { get; set; }

        public CreateStudyYearCommand(CreateStudyYearDto studyYearDto)
        {
            StudyYearDto = studyYearDto;
        }
    }
}