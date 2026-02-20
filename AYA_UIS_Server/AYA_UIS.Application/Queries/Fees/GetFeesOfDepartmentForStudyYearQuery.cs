using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module.FeeDtos;

namespace AYA_UIS.Application.Queries.Fees
{
    public class GetFeesOfDepartmentForStudyYearQuery : IRequest<List<FeeDto>>
    {
        public int DepartmentId { get; set; }
        public int StudyYearId { get; set; }

        public GetFeesOfDepartmentForStudyYearQuery(int departmentId, int studyYearId)
        {
            DepartmentId = departmentId;
            StudyYearId = studyYearId;
        }
    }
}