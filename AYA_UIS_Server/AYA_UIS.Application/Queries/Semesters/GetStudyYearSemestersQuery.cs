using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module.SemesterDtos;

namespace AYA_UIS.Application.Queries.Semesters
{
    public class GetStudyYearSemestersQuery : IRequest<List<SemesterDto>>
    {
        public int StudyYearId { get; set; }

        public GetStudyYearSemestersQuery(int studyYearId)
        {
            StudyYearId = studyYearId;
        }
    }
}