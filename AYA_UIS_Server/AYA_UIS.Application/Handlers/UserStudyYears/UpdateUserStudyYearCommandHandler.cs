using AYA_UIS.Application.Commands.UserStudyYears;
using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using MediatR;
using Shared.Dtos.Info_Module.UserStudyYearDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Handlers.UserStudyYears
{
    public class UpdateUserStudyYearCommandHandler : IRequestHandler<UpdateUserStudyYearCommand, Response<UserStudyYearDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserStudyYearCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<UserStudyYearDto>> Handle(UpdateUserStudyYearCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.UserStudyYears.GetByIdAsync(request.Id);
            if (entity is null)
                return Response<UserStudyYearDto>.ErrorResponse("User study year record not found.");

            var dto = request.Dto;

            if (dto.Level.HasValue)
                entity.Level = dto.Level.Value;

            if (dto.IsCurrent.HasValue)
            {
                // If setting as current, unset any other current record for this user
                if (dto.IsCurrent.Value)
                {
                    var currentRecord = await _unitOfWork.UserStudyYears.GetCurrentByUserIdAsync(entity.UserId);
                    if (currentRecord is not null && currentRecord.Id != entity.Id)
                    {
                        var trackedCurrent = await _unitOfWork.UserStudyYears.GetByIdAsync(currentRecord.Id);
                        if (trackedCurrent is not null)
                        {
                            trackedCurrent.IsCurrent = false;
                            await _unitOfWork.UserStudyYears.Update(trackedCurrent);
                        }
                    }
                }
                entity.IsCurrent = dto.IsCurrent.Value;
            }

            await _unitOfWork.UserStudyYears.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            // Re-fetch with includes
            var updated = await _unitOfWork.UserStudyYears.GetByUserAndStudyYearAsync(entity.UserId, entity.StudyYearId);

            return Response<UserStudyYearDto>.SuccessResponse(MapToDto(updated!));
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
