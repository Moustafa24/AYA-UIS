using AYA_UIS.Application.Commands.Fees;
using AYA_UIS.Core.Domain.Entities.Models;
using AYA_UIS.Shared.Exceptions;
using Domain.Contracts;
using MediatR;

namespace AYA_UIS.Application.Handlers.Fees
{
    public class CreateFeeCommandHandler : IRequestHandler<CreateFeeCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateFeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateFeeCommand request, CancellationToken cancellationToken)
        {
            var departmentFee = await _unitOfWork.DepartmentFees.GetByIdAsync(request.DepartmentFeeId);
            if (departmentFee is null)
                throw new NotFoundException($"Department fee with ID {request.DepartmentFeeId} not found.");

            var fee = new Fee
            {
                Amount = request.Amount,
                Type = request.Type,
                DepartmentFeeId = request.DepartmentFeeId
            };

            await _unitOfWork.Fees.AddAsync(fee);
            await _unitOfWork.SaveChangesAsync();

            return fee.Id;
        }
    }
}
