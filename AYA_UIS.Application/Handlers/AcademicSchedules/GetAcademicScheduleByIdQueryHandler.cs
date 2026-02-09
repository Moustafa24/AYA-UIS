using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Application.Queries.AcademicSchedules;
using AYA_UIS.Shared.Exceptions;
using Domain.Contracts;
using MediatR;
using Shared.Dtos.Info_Module.AcademicSheduleDtos;

namespace AYA_UIS.Application.Handlers.AcademicSchedules
{
    public class GetAcademicScheduleByIdQueryHandler : IRequestHandler<GetAcademicScheduleByIdQuery, AcademicScheduleDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAcademicScheduleByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AcademicScheduleDto> Handle(GetAcademicScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            var schedule = await _unitOfWork.AcademicSchedules.GetByIdAsync(request.Id);

            if (schedule == null)
                throw new NotFoundException($"Academic Schedule with id {request.Id} not found.");

            var result = new AcademicScheduleDto
            {
                Id = schedule.Id,
                Title = schedule.Title,
                Url = schedule.Url,
                Description = schedule.Description,
                CreatedAt = schedule.CreatedAt
            };

            return result;
        }
    }
}