using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module.CourseDtos;

namespace AYA_UIS.Application.Queries.Courses
{
    public class GetDepartmentOpenCoursesQuery : IRequest<IEnumerable<CourseDto>>
    {
        public int DepartmentId {get; set;}

        public GetDepartmentOpenCoursesQuery(int departmentId)
        {
            DepartmentId = departmentId;
        }
    }
}