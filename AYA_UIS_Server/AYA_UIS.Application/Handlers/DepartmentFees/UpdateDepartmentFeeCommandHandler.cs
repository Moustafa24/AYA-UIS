using AYA_UIS.Application.Commands.DepartmentFees;
using AutoMapper;
using AYA_UIS.Core.Domain.Entities.Models;
using AYA_UIS.Shared.Exceptions;
using Domain.Contracts;
using MediatR;

namespace AYA_UIS.Application.Handlers.DepartmentFees;

public class UpdateDepartmentFeeCommandHandler : IRequestHandler<UpdateDepartmentFeeCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateDepartmentFeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(
        UpdateDepartmentFeeCommand request, 
        CancellationToken cancellationToken)
    {
        var departmentFee = await _unitOfWork.DepartmentFees
            .GetByCompositeKeyAsync(request.DepartmentName, request.GradeYear);

        if (departmentFee is null)
            throw new NotFoundException(
                $"Department fee for '{request.DepartmentName}' year {request.GradeYear} not found.");

        // Update fees
        departmentFee.Fees.Clear();
        foreach (var feeDto in request.Fees)
        {
            departmentFee.Fees.Add(new Fee
            {
                Type = feeDto.Type,
                Amount = feeDto.Amount,
                DepartmentFeeId = departmentFee.Id
            });
        }

        await _unitOfWork.DepartmentFees.Update(departmentFee);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
