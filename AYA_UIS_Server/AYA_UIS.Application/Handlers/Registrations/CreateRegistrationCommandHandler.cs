using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Application.Commands.Registrations;
using AYA_UIS.Core.Domain.Entities.Identity;
using AYA_UIS.Core.Domain.Entities.Models;
using AYA_UIS.Core.Domain.Enums;
using AYA_UIS.Shared.Exceptions;
using Domain.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos.Info_Module.RegistrationDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Handlers.Registrations
{
    public class CreateRegistrationCommandHandler : IRequestHandler<CreateRegistrationCommand, Response<RegistrationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public CreateRegistrationCommandHandler(
            IUnitOfWork unitOfWork,
            UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Response<RegistrationDto>> Handle(CreateRegistrationCommand request, CancellationToken cancellationToken)
        {
            // Validate course exists
            var course = await _unitOfWork.Courses.GetByIdAsync(request.RegistrationDto.CourseId);
            if (course == null)
                throw new NotFoundException("Course not found");

            // Validate study year exists
            var studyYear = await _unitOfWork.StudyYears.GetByIdAsync(request.RegistrationDto.StudyYearId);
            if (studyYear == null)
                throw new NotFoundException("Study year not found");

            // Validate semester exists
            var semester = await _unitOfWork.Semesters.GetByIdAsync(request.RegistrationDto.SemesterId);
            if (semester == null)
                throw new NotFoundException("Semester not found");

            // Validate user exists
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                throw new NotFoundException("User not found");

            // Check if user is already registered for this course in this study year
            var existingRegistration = await _unitOfWork.Registrations.GetByUserAndCourseAsync(request.UserId, request.RegistrationDto.CourseId, request.RegistrationDto.StudyYearId);
            if (existingRegistration != null)
                throw new ConflictException("User is already registered for this course");

            // Check prerequisites
            var prerequisites = await _unitOfWork.Courses.GetPrerequisitesAsync(request.RegistrationDto.CourseId);
            foreach (var prereq in prerequisites)
            {
                var prereqRegistration = await _unitOfWork.Registrations.GetByUserAndCourseAsync(request.UserId, prereq.PrerequisiteCourseId, request.RegistrationDto.StudyYearId);
                if (prereqRegistration == null || prereqRegistration.Status != Statuses.Openned || prereqRegistration.Grade < Grads.C)
                    throw new BadRequestException($"Prerequisite course {prereq.PrerequisiteCourse.Code} not satisfied");
            }

            // Check credit limits
            var currentRegistrations = await _unitOfWork.Registrations.GetByUserAndStudyYearAsync(request.UserId, request.RegistrationDto.StudyYearId);
            int currentCredits = currentRegistrations.Where(r => r.Status == Statuses.Openned).Sum(r => r.Course.Credits);
            if (currentCredits + course.Credits > user.AllowedCredits)
                throw new BadRequestException("Credit limit exceeded");

            // Create registration
            var registration = new Registration
            {
                Status = Statuses.Pending,
                UserId = request.UserId,
                CourseId = request.RegistrationDto.CourseId,
                StudyYearId = request.RegistrationDto.StudyYearId,
                SemesterId = request.RegistrationDto.SemesterId,
                RegisteredAt = DateTime.UtcNow
            };

            await _unitOfWork.Registrations.AddAsync(registration);
            await _unitOfWork.SaveChangesAsync();

            var dto = new RegistrationDto
            {
                Id = registration.Id,
                Status = registration.Status,
                UserId = registration.UserId,
                CourseId = registration.CourseId,
                CourseName = course.Name,
                CourseCode = course.Code,
                CourseCredits = course.Credits,
                StudyYearId = registration.StudyYearId,
                SemesterId = registration.SemesterId,
                RegisteredAt = registration.RegisteredAt
            };

            return Response<RegistrationDto>.SuccessResponse(dto);
        }
    }
}
