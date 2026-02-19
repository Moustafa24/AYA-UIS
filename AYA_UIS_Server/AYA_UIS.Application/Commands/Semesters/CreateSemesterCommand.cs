using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module.AcademicSheduleDtos;
using Shared.Dtos.Info_Module.SemesterDtos;

namespace AYA_UIS.Application.Commands.Semesters
{
    public class CreateSemesterCommand : IRequest<int>
    {
        public int StudyYearId { get; set; }
        public CreateSemesterDto SemesterDto { get; set; }

        public CreateSemesterCommand(int studyYearId, CreateSemesterDto semesterDto)
        {
            StudyYearId = studyYearId;
            SemesterDto = semesterDto;
        }
    }
}