using AYA_UIS.Application.Queries.UserStudyYears;
using AYA_UIS.Core.Domain.Entities.Models;
using AYA_UIS.Core.Domain.Enums;
using Domain.Contracts;
using MediatR;
using Shared.Dtos.Info_Module.UserStudyYearDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Handlers.UserStudyYears
{
    public class GetUserStudyYearTimelineQueryHandler : IRequestHandler<GetUserStudyYearTimelineQuery, Response<UserStudyYearTimelineDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserStudyYearTimelineQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<UserStudyYearTimelineDto>> Handle(GetUserStudyYearTimelineQuery request, CancellationToken cancellationToken)
        {
            var records = (await _unitOfWork.UserStudyYears.GetByUserIdAsync(request.UserId)).ToList();

            if (!records.Any())
                return Response<UserStudyYearTimelineDto>.ErrorResponse("No study year records found for this user.");

            var current = records.FirstOrDefault(r => r.IsCurrent);
            var completedYears = records.Where(r => !r.IsCurrent).ToList();

            var timeline = new UserStudyYearTimelineDto
            {
                UserId = request.UserId,
                CurrentLevel = current?.Level ?? records.Last().Level,
                CurrentLevelName = (current?.Level ?? records.Last().Level).ToString().Replace("_", " "),
                CurrentStudyYearId = current?.StudyYearId,
                CurrentStartYear = current?.StudyYear?.StartYear,
                CurrentEndYear = current?.StudyYear?.EndYear,
                TotalYearsCompleted = completedYears.Count,
                IsGraduated = records.Any(r => r.Level == Levels.Graduate),
                StudyYears = records.Select(MapToDto).ToList()
            };

            return Response<UserStudyYearTimelineDto>.SuccessResponse(timeline);
        }

        private static UserStudyYearDto MapToDto(UserStudyYear entity)
        {
            return new UserStudyYearDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                StudyYearId = entity.StudyYearId,
                StartYear = entity.StudyYear?.StartYear ?? 0,
                EndYear = entity.StudyYear?.EndYear ?? 0,
                DepartmentName = entity.StudyYear?.Department?.Name ?? string.Empty,
                Level = entity.Level,
                LevelName = entity.Level.ToString().Replace("_", " "),
                IsCurrent = entity.IsCurrent,
                EnrolledAt = entity.EnrolledAt
            };
        }
    }
}
