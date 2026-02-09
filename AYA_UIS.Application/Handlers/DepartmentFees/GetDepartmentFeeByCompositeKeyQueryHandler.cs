using AYA_UIS.Application.Queries.DepartmentFees;
using MediatR;
using AYA_UIS.Core.Abstractions.Contracts;
using Shared.Dtos.Info_Module;

namespace AYA_UIS.Application.Handlers.DepartmentFees;

public class GetDepartmentFeeByCompositeKeyQueryHandler 
    : IRequestHandler<GetDepartmentFeeByCompositeKeyQuery, DepartmentFeeDtos>
{
    private readonly IDepartmentFeeService _service;

    public GetDepartmentFeeByCompositeKeyQueryHandler(IDepartmentFeeService service)
    {
        _service = service;
    }

    public async Task<DepartmentFeeDtos> Handle(
        GetDepartmentFeeByCompositeKeyQuery request, 
        CancellationToken cancellationToken)
    {
        return await _service.GetDepartmentFeeByCompositeKeyAsync(
            request.DepartmentName, 
            request.GradeYear);
    }
}
