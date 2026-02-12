using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AYA_UIS.Application.Commands.Courses;
using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using MediatR;
using Shared.Dtos.Info_Module.CourseDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Handlers.Courses
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Response<CourseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCourseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<CourseDto>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = _mapper.Map<Course>(request.Course);

            var department = await _unitOfWork.Departments.GetByIdAsync(request.Course.DepartmentId);
            if (department is null)                
                throw new Exception($"Department with ID {request.Course.DepartmentId} not found.");

            await _unitOfWork.Courses.AddAsync(course);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<CourseDto>(course);
            return Response<CourseDto>.SuccessResponse(result);
        }
    }
}