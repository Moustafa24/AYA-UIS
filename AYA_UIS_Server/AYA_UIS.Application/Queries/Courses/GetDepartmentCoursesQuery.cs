using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module.CourseDtos;

namespace AYA_UIS.Application.Queries.Courses
{
    public class GetDepartmentCoursesQuery : IRequest<List<CourseDto>>
    {
        public int DepartmentId { get; set; }

        public GetDepartmentCoursesQuery(int departmentId)
        {
            DepartmentId = departmentId;
        }
    }
}