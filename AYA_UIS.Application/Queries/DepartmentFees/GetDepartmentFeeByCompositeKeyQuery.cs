using MediatR;
using Shared.Dtos.Info_Module.DepartmentFeeDtos;

namespace AYA_UIS.Application.Queries.DepartmentFees;

public record GetDepartmentFeeByCompositeKeyQuery(string DepartmentName, int GradeYear) 
    : IRequest<DepartmentFeeDto>;
