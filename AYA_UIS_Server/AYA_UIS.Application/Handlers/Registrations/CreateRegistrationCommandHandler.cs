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
using Microsoft.EntityFrameworkCore; 
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos.Info_Module.RegistrationDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Handlers.Registrations
{
    public class CreateRegistrationCommandHandler : IRequestHandler<CreateRegistrationCommand, Response<int>>
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

        public async Task<Response<int>> Handle(CreateRegistrationCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Get user
            var user = await _userManager.Users
                .Include(u => u.Registrations)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user == null)
                throw new NotFoundException("User not found");

            // 2️⃣ Get course
            var course = await _unitOfWork.Courses.GetByIdAsync(request.RegistrationDto.CourseId);

            if (course == null)
                throw new NotFoundException("Course not found");

            // 3️⃣ Check course open
            if (course.Status != CourseStatus.Opened)
                throw new BadRequestException("Course is closed");

            // 4️⃣ Already registered?
            if (user.Registrations.Any(r => r.CourseId == request.RegistrationDto.CourseId))
                throw new BadRequestException("Already registered in this course");

            // 5️⃣ Get prerequisite IDs
            var prerequisiteIds = course.PrerequisiteFor
                .Select(p => p.PrerequisiteCourseId)
                .ToList();

            // 6️⃣ Get passed courses
            var passedCourseIds = user.Registrations
                .Where(r => r.IsPassed)
                .Select(r => r.CourseId)
                .ToList();

            var missingCourses = prerequisiteIds.Except(passedCourseIds);

            if (missingCourses.Any())
                throw new BadRequestException("Prerequisites not completed");

            // 7️⃣ Check credit hours
            if (user.AllowedCredits < course.Credits)
                throw new BadRequestException("Not enough credit hours");

            // 8️⃣ Create registration
            var registration = new Registration
            {
                UserId = user.Id,
                CourseId = course.Id,
                StudyYearId = request.RegistrationDto.StudyYearId,
                SemesterId = request.RegistrationDto.SemesterId,
                Status = RegistrationStatus.Pending
            };

            await _unitOfWork.Registrations.AddAsync(registration);
            await _unitOfWork.SaveChangesAsync();

            return new Response<int>()
            {
                Data = registration.Id,
                Success = true,
                Errors = null,
                Message = "Registration created successfully"
            };
        }
    }
}
