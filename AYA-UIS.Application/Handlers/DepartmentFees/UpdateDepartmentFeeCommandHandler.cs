using AYA_UIS.Application.Commands.DepartmentFees;
using MediatR;
using Services.Abstraction.Contracts;

namespace AYA_UIS.Application.Handlers.DepartmentFees;

public class UpdateDepartmentFeeCommandHandler : IRequestHandler<UpdateDepartmentFeeCommand, Unit>
{
    private readonly IDepartmentFeeService _service;

    public UpdateDepartmentFeeCommandHandler(IDepartmentFeeService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(
        UpdateDepartmentFeeCommand request, 
        CancellationToken cancellationToken)
    {
        await _service.UpdateByCompositeKeyAsync(
            request.DepartmentName, 
            request.GradeYear, 
            request.Dto);
            
        return Unit.Value;
    }
}
