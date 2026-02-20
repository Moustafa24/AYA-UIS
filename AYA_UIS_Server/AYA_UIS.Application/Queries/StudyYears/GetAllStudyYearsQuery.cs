using MediatR;
using Shared.Dtos.Info_Module.StudyYearDtos;

namespace AYA_UIS.Application.Queries.StudyYears;

public record GetAllStudyYearsQuery : IRequest<IEnumerable<StudyYearDto>>;
