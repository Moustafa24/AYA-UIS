using MediatR;
using Shared.Dtos.Info_Module;

namespace AYA_UIS.Application.Commands.DepartmentFees;

public record UpdateDepartmentFeeCommand(
    string DepartmentName, 
    string GradeYear, 
    DepartmentFeeDtos Dto) : IRequest<Unit>;
