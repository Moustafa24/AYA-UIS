using MediatR;
using Shared.Dtos.Info_Module.DepartmentFeeDtos;
using Shared.Dtos.Info_Module.FeeDtos;

namespace AYA_UIS.Application.Commands.DepartmentFees;

public record UpdateDepartmentFeeCommand(
    string DepartmentName,
    int GradeYear,
    List<FeeDto> Fees
) : IRequest<Unit>;
