using MediatR;
using Shared.Dtos.Info_Module.DepartmentDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Commands.Departments
{
    public record CreateDepartmentCommand : IRequest<Response<DepartmentDto>>
    {
        public CreateDepartmentDto Department { get; init; } = null!;
    }
}