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

            // 3️⃣ Check study year is current
            var studyYear = await _unitOfWork.StudyYears.IsCurrentStudyYearAsync(request.RegistrationDto.StudyYearId);

            if (!studyYear)
                throw new BadRequestException("You can only register in the current study year, this study year is ended");

            // 4️⃣ Check semester belongs to the study year
            var isSemesterInStudyYear = await _unitOfWork.Semesters.IsSemesterBelongsToStudyYearAsync(request.RegistrationDto.SemesterId, request.RegistrationDto.StudyYearId);

            if (!isSemesterInStudyYear)
                throw new BadRequestException("The semester does not belong to the specified study year");

            // 5️⃣ Check semester is active
            var semester = await _unitOfWork.Semesters.IsActiveSemesterAsync(request.RegistrationDto.SemesterId);

            if (!semester)
                throw new BadRequestException("You can only register in the active semester, this semester is ended");

            // 6️⃣ Check course is open
            if (course.Status != CourseStatus.Opened)
                throw new BadRequestException("Course registration is closed");

            // 7️⃣ Already registered?
            var isRegistrationExists = await _unitOfWork.Registrations.IsUserRegisteredInCourseAsync(user.Id, course.Id);

            if (isRegistrationExists)
                throw new BadRequestException("Already registered in this course");

            // 8️⃣ Check prerequisites
            var prerequisitesCourses = await _unitOfWork.Courses.GetCoursePrerequisitesAsync(course.Id);

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

            // 9️⃣ Check credit hours
            if (user.AllowedCredits < course.Credits)
                throw new BadRequestException("Not enough credit hours");

            // 🔟 Create registration
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

            // 1️⃣1️⃣ Deduct credit hours
            user.AllowedCredits -= course.Credits;
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                throw new BadRequestException("Failed to update user credit hours");

            return registration.Id;
        }
    }
}