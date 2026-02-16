using MediatR;
using Shared.Dtos.Info_Module.UserStudyYearDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Queries.UserStudyYears
{
    public class GetUserStudyYearTimelineQuery : IRequest<Response<UserStudyYearTimelineDto>>
    {
        public string UserId { get; set; } = string.Empty;

        public GetUserStudyYearTimelineQuery(string userId)
        {
            UserId = userId;
        }
    }
}
