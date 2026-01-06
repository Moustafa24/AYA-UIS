using MediatR;
using Shared.Dtos.Info_Module;

namespace AYA_UIS.Application.Queries.DepartmentFees;

public record GetDepartmentFeeByCompositeKeyQuery(string DepartmentName, string GradeYear) 
    : IRequest<DepartmentFeeDtos>;
