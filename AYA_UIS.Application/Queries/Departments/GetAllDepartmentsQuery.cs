using MediatR;
using Shared.Dtos.Info_Module.DepartmentDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Queries.Departments
{
    public record GetAllDepartmentsQuery : IRequest<Response<IEnumerable<DepartmentDto>>>;
}
