using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module.FeeDtos;

namespace AYA_UIS.Application.Queries.Fees
{
    public class GetFeesOfStudyYearQuery : IRequest<List<FeeDto>>
    {
        public int StudyYearId { get; set; }

        public GetFeesOfStudyYearQuery(int studyYearId)
        {
            StudyYearId = studyYearId;
        }
    }
}