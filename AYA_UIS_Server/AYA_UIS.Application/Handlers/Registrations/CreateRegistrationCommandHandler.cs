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
    public class CreateRegistrationCommandHandler : IRequestHandler<CreateRegistrationCommand, int>
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

        public async Task<int> Handle(CreateRegistrationCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Get user
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user == null)
                throw new NotFoundException("User not found");

            // 2️⃣ Get course
            var course = await _unitOfWork.Courses.GetByIdAsync(request.RegistrationDto.CourseId);

            if (course == null)
                throw new NotFoundException("Course not found");

            // 3️⃣ Check course open
            if (course.Status != CourseStatus.Opened)
                throw new BadRequestException("Course is closed");

            var isRegistrationExists = await _unitOfWork.Registrations.IsUserRegisteredInCourseAsync(user.Id, course.Id);

            // 4️⃣ Already registered?
            if (isRegistrationExists)
                throw new BadRequestException("Already registered in this course");

            // 5️⃣ Get prerequisite IDs
            var prerequisitesCourses = await _unitOfWork.Courses.GetCoursePrerequisitesAsync(course.Id);

            // 6️⃣ Get passed courses
            var passedCourseIds = new List<int>();
            foreach (var preq in prerequisitesCourses)
            {
                var isPassed = await _unitOfWork.Registrations.IsCourseCompletedByUserAsync(user.Id, preq.Id);
                if (isPassed)
                {
                    passedCourseIds.Add(preq.Id);
                }
            }

            var missingCourses = prerequisitesCourses.Select(p => p.Id).Except(passedCourseIds);

            if (missingCourses.Any())
                throw new BadRequestException("Prerequisites not completed");

            // 7️⃣ Check credit hours
            if (user.AllowedCredits < course.Credits)
                throw new BadRequestException("Not enough credit hours");

            // 8️⃣ Validate current study year and semester
            var currentUserStudyYear = await _unitOfWork.UserStudyYears
                .GetCurrentByUserIdAsync(user.Id);

            if (currentUserStudyYear == null)
                throw new BadRequestException("No active study year found for user");

            if (currentUserStudyYear.StudyYearId != request.RegistrationDto.StudyYearId)
                throw new BadRequestException("You can only register in your current study year");

            // Verify the semester belongs to the current study year
            var semester = await _unitOfWork.Semesters.GetByIdAsync(request.RegistrationDto.SemesterId);
            
            if (semester == null)
                throw new NotFoundException("Semester not found");

            if (semester.StudyYearId != currentUserStudyYear.StudyYearId)
                throw new BadRequestException("This semester does not belong to your current study year");

            if (!semester.IsActive)
                throw new BadRequestException("Cannot register in an inactive semester. Registration is only allowed in the current active semester");

            // 9️⃣ Create registration
            var registration = new Registration
            {
                UserId = user.Id,
                CourseId = course.Id,
                StudyYearId = request.RegistrationDto.StudyYearId,
                SemesterId = request.RegistrationDto.SemesterId,
                Status = RegistrationStatus.Pending,
                Grade = null,
                IsPassed = false
            };

            await _unitOfWork.Registrations.AddAsync(registration);
            await _unitOfWork.SaveChangesAsync();

            return registration.Id;
        }
    }
}
