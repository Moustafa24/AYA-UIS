using MediatR;

namespace AYA_UIS.Application.Commands.Departments
{
    public record DeleteDepartmentCommand(int Id) : IRequest<Unit>;
}