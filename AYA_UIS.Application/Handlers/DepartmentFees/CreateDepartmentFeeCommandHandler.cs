using AYA_UIS.Application.Commands.DepartmentFees;
using AYA_UIS.Core.Domain.Entities.Models;
using AYA_UIS.Shared.Exceptions;
using Domain.Contracts;
using MediatR;

namespace AYA_UIS.Application.Handlers.DepartmentFees
{
    public class CreateDepartmentFeeCommandHandler : IRequestHandler<CreateDepartmentFeeCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDepartmentFeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateDepartmentFeeCommand request, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(request.DepartmentId);
            if (department is null)
                throw new NotFoundException($"Department with ID {request.DepartmentId} not found.");

            var studyYear = await _unitOfWork.StudyYears.GetByIdAsync(request.StudyYearId);
            if (studyYear is null)
                throw new NotFoundException($"Study year with ID {request.StudyYearId} not found.");

            var departmentFee = new DepartmentFee
            {
                DepartmentId = request.DepartmentId,
                StudyYearId = request.StudyYearId
            };

            await _unitOfWork.DepartmentFees.AddAsync(departmentFee);
            await _unitOfWork.SaveChangesAsync();

            return departmentFee.Id;
        }
    }
}
