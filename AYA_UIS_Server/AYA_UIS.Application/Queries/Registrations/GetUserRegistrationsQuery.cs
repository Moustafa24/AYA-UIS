using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module.RegistrationDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Queries.Registrations
{
    public class GetUserRegistrationsQuery : IRequest<Response<List<RegistrationDto>>>
    {
        public string UserId { get; set; } = string.Empty;
        public int? StudyYearId { get; set; }

        public GetUserRegistrationsQuery(string userId, int? studyYearId = null)
        {
            UserId = userId;
            StudyYearId = studyYearId;
        }
    }
}