using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module;

namespace AYA_UIS.Application.Queries.AcademicSchedules
{
    public class GetAllAcademicSchedulesQuery : IRequest<List<AcademicSchedulesDto>>
    {
        
    }
}