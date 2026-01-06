using MediatR;
using Shared.Dtos.Info_Module;

namespace AYA_UIS.Application.Queries.DepartmentFees;

public record GetAllDepartmentFeesQuery : IRequest<IEnumerable<DepartmentFeeDtos>>;
