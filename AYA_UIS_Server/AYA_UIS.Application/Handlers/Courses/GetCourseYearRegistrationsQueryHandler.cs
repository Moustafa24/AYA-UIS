using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Application.Queries.Courses;
using Domain.Contracts;
using MediatR;
using Shared.Dtos.Info_Module.CourseDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Handlers.Courses
{
    public class GetCourseYearRegistrationsQueryHandler : IRequestHandler<GetCourseYearRegistrationsQuery, Response<CourseWithRegistrationsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCourseYearRegistrationsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<CourseWithRegistrationsDto>> Handle(GetCourseYearRegistrationsQuery request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.Courses.GetYearCourseRegistrationAsync(request.CourseId, request.YearId);
            
            if (course is null)
                return Response<CourseWithRegistrationsDto>.ErrorResponse("Course not found");

            var result = new CourseWithRegistrationsDto
            {
                Id = course.Id,
                Code = course.Code,
                Name = course.Name,
                Credits = course.Credits,
                StudyYearId = request.YearId,
                StudentRegistrations = course.Registrations.Select(r => new StudentRegistrationDto
                {
                    UserId = r.UserId,
                    DisplayName = r.User.DisplayName,
                    AcademicCode = r.User.Academic_Code,
                    Email = r.User.Email ?? string.Empty,
                    RegisteredAt = r.RegisteredAt
                }).ToList()
            };

            return Response<CourseWithRegistrationsDto>.SuccessResponse(result);
        }
    }
}
