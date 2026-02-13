using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module.RegistrationDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Queries.Registrations
{
    public class GetAllRegistrationsQuery : IRequest<Response<List<RegistrationDto>>>
    {
        public int? CourseId { get; set; }
        public int? StudyYearId { get; set; }
        public int? SemesterId { get; set; }
        public string? UserId { get; set; }

        public GetAllRegistrationsQuery(int? courseId = null, int? studyYearId = null, int? semesterId = null, string? userId = null)
        {
            CourseId = courseId;
            StudyYearId = studyYearId;
            SemesterId = semesterId;
            UserId = userId;
        }
    }
}