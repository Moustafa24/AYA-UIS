using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AYA_UIS.Application.Queries.AcademicSchedules;
using AYA_UIS.Shared.Exceptions;
using Domain.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Dtos.Info_Module.AcademicSheduleDtos;

namespace AYA_UIS.Application.Handlers.AcademicSchedules
{
    public class GetAcademicSchedulesBySemesterIdQueryHandler : IRequestHandler<GetAcademicSchedulesBySemesterIdQuery, IEnumerable<AcademicScheduleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAcademicSchedulesBySemesterIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
    }

        public async Task<IEnumerable<AcademicScheduleDto>> Handle(GetAcademicSchedulesBySemesterIdQuery request, CancellationToken cancellationToken)
        {
            // check if semester exists
            var semester = await _unitOfWork.Semesters.GetByIdAsync(request.SemesterId);
            if (semester is null)                
                throw new NotFoundException("Semester not found");
            
            var schedules = await _unitOfWork.AcademicSchedules.GetBySemesterIdAsync(request.SemesterId);
            return _mapper.Map<IEnumerable<AcademicScheduleDto>>(schedules);
        }
    }
}