using MediatR;
using Shared.Dtos.Info_Module.UserStudyYearDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Queries.UserStudyYears
{
    public class GetCurrentUserStudyYearQuery : IRequest<Response<UserStudyYearDto>>
    {
        public string UserId { get; set; } = string.Empty;

        public GetCurrentUserStudyYearQuery(string userId)
        {
            UserId = userId;
        }
    }
}
