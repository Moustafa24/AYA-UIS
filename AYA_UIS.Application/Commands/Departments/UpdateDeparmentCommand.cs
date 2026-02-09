using MediatR;
using Shared.Dtos.Info_Module.DepartmentDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Commands.Departments
{
    public record UpdateDeparmentCommand(int Id, UpdateDepartmentDto Department) : IRequest<Response<DepartmentDto>>;
}