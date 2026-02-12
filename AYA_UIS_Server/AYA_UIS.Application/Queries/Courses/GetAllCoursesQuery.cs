using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module.CourseDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Queries.Courses
{
    public record GetAllCoursesQuery(): IRequest<Response<IEnumerable<CourseDto>>>;
}