using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Application.Commands.Registrations;
using AYA_UIS.Shared.Exceptions;
using Domain.Contracts;
using MediatR;
using Shared.Dtos.Info_Module.RegistrationDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Handlers.Registrations
{
    public class UpdateRegistrationStatusCommandHandler : IRequestHandler<UpdateRegistrationStatusCommand, Response<RegistrationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRegistrationStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<RegistrationDto>> Handle(UpdateRegistrationStatusCommand request, CancellationToken cancellationToken)
        {
            var registration = await _unitOfWork.Registrations.GetByIdAsync(request.RegistrationId);
            if (registration == null)
                throw new NotFoundException("Registration not found");

            registration.Status = request.UpdateDto.Status;
            registration.Reason = request.UpdateDto.Reason;
            if (request.UpdateDto.Grade.HasValue)
                registration.Grade = request.UpdateDto.Grade.Value;

            await _unitOfWork.Registrations.Update(registration);
            await _unitOfWork.SaveChangesAsync();

            var course = await _unitOfWork.Courses.GetByIdAsync(registration.CourseId);

            var dto = new RegistrationDto
            {
                Id = registration.Id,
                Status = registration.Status,
                Reason = registration.Reason,
                Grade = registration.Grade,
                UserId = registration.UserId,
                CourseId = registration.CourseId,
                CourseName = course?.Name ?? "",
                CourseCode = course?.Code ?? "",
                CourseCredits = course?.Credits ?? 0,
                StudyYearId = registration.StudyYearId,
                SemesterId = registration.SemesterId,
                RegisteredAt = registration.RegisteredAt
            };

            return Response<RegistrationDto>.SuccessResponse(dto);
        }
    }
}
