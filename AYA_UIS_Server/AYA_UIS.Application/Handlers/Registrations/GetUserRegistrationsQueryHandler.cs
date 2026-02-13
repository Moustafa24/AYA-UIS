using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Application.Queries.Registrations;
using Domain.Contracts;
using MediatR;
using Shared.Dtos.Info_Module.RegistrationDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Handlers.Registrations
{
    public class GetUserRegistrationsQueryHandler : IRequestHandler<GetUserRegistrationsQuery, Response<List<RegistrationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserRegistrationsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<List<RegistrationDto>>> Handle(GetUserRegistrationsQuery request, CancellationToken cancellationToken)
        {
            var registrations = await _unitOfWork.Registrations.GetByUserAsync(request.UserId, request.StudyYearId);

            var dtos = registrations.Select(r => new RegistrationDto
            {
                Id = r.Id,
                Status = r.Status,
                Reason = r.Reason,
                Grade = r.Grade,
                UserId = r.UserId,
                CourseId = r.CourseId,
                CourseName = r.Course.Name,
                CourseCode = r.Course.Code,
                CourseCredits = r.Course.Credits,
                StudyYearId = r.StudyYearId,
                SemesterId = r.SemesterId,
                RegisteredAt = r.RegisteredAt
            }).ToList();

            return Response<List<RegistrationDto>>.SuccessResponse(dtos);
        }
    }
}
