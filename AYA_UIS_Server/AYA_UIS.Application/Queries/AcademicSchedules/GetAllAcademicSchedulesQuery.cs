using MediatR;
using Shared.Dtos.Info_Module.AcademicSheduleDtos;

namespace AYA_UIS.Application.Queries.AcademicSchedules
{
    public class GetAllAcademicSchedulesQuery : IRequest<List<AcademicSchedulesDto>>
    {
    }
}