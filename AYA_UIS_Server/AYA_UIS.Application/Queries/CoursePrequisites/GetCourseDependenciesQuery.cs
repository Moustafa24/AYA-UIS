using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module.CourseDtos;

namespace AYA_UIS.Application.Queries.CoursePrequisites
{
    public class GetCourseDependenciesQuery : IRequest<List<CourseDto>>
    {
        public int CourseId { get; set; }
        public GetCourseDependenciesQuery(int courseId)
        {
            CourseId = courseId;
        }
    }
}