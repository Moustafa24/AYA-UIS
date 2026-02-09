using AYA_UIS.Application.Queries.DepartmentFees;
using MediatR;
using AYA_UIS.Core.Abstractions.Contracts;
using Shared.Dtos.Info_Module;

namespace AYA_UIS.Application.Handlers.DepartmentFees;

public class GetAllDepartmentFeesQueryHandler 
    : IRequestHandler<GetAllDepartmentFeesQuery, IEnumerable<DepartmentFeeDtos>>
{
    private readonly IDepartmentFeeService _service;

    public GetAllDepartmentFeesQueryHandler(IDepartmentFeeService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<DepartmentFeeDtos>> Handle(
        GetAllDepartmentFeesQuery request, 
        CancellationToken cancellationToken)
    {
        return await _service.GetAllDepartmentFeeAsync();
    }
}
