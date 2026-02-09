using MediatR;
using Shared.Dtos.Info_Module.DepartmentDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Queries.Departments
{
    public record GetDepartmentByIdQuery(int Id) : IRequest<Response<DepartmentDto>>;
}
