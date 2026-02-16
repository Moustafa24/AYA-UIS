using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Application.Queries.Courses;
using Domain.Contracts;
using MediatR;
using Shared.Dtos.Info_Module.CourseDtos;

namespace AYA_UIS.Application.Handlers.Courses
{
    public class GetDepartmentCoursesQueryHandler : IRequestHandler<GetDepartmentCoursesQuery, List<CourseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDepartmentCoursesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CourseDto>> Handle(GetDepartmentCoursesQuery request, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(request.DepartmentId);
            if (department == null)            {
                throw new Exception("Department not found");
            }
            var courses = await _unitOfWork.Courses.GetDepartmentCoursesAsync(request.DepartmentId);
            return courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code,
                Credits = c.Credits
            }).ToList();
        }
    }
}