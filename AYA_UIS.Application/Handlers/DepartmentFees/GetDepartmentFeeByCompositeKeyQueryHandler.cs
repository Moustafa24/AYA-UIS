using AYA_UIS.Application.Queries.DepartmentFees;
using AutoMapper;
using AYA_UIS.Shared.Exceptions;
using Domain.Contracts;
using MediatR;
using Shared.Dtos.Info_Module.DepartmentFeeDtos;

namespace AYA_UIS.Application.Handlers.DepartmentFees;

public class GetDepartmentFeeByCompositeKeyQueryHandler 
    : IRequestHandler<GetDepartmentFeeByCompositeKeyQuery, DepartmentFeeDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDepartmentFeeByCompositeKeyQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DepartmentFeeDto> Handle(
        GetDepartmentFeeByCompositeKeyQuery request, 
        CancellationToken cancellationToken)
    {
        var departmentFee = await _unitOfWork.DepartmentFees
            .GetByCompositeKeyAsync(request.DepartmentName, request.GradeYear);

        if (departmentFee is null)
            throw new NotFoundException(
                $"Department fee for '{request.DepartmentName}' year {request.GradeYear} not found.");

        return _mapper.Map<DepartmentFeeDto>(departmentFee);
    }
}
