using AYA_UIS.Application.Queries.DepartmentFees;
using AutoMapper;
using Domain.Contracts;
using MediatR;
using Shared.Dtos.Info_Module.DepartmentFeeDtos;

namespace AYA_UIS.Application.Handlers.DepartmentFees;

public class GetAllDepartmentFeesQueryHandler 
    : IRequestHandler<GetAllDepartmentFeesQuery, IEnumerable<DepartmentFeeDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllDepartmentFeesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DepartmentFeeDto>> Handle(
        GetAllDepartmentFeesQuery request, 
        CancellationToken cancellationToken)
    {
        var departmentFees = await _unitOfWork.DepartmentFees.GetAllWithDetailsAsync();
        return _mapper.Map<IEnumerable<DepartmentFeeDto>>(departmentFees);
    }
}
