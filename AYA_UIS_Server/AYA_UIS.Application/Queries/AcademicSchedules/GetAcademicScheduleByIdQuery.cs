using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module.AcademicSheduleDtos;

namespace AYA_UIS.Application.Queries.AcademicSchedules
{
    public record GetAcademicScheduleByIdQuery : IRequest<AcademicScheduleDto>
    {
        public int Id { get; init; }

        public GetAcademicScheduleByIdQuery(int id)
        {
            Id = id;
        }
    }
}