using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Dtos.Info_Module.AcademicSheduleDtos;

namespace AYA_UIS.Application.Commands.AcademicSchedules
{
    public record CreateSemesterAcademicScheduleCommand(
        string UploadedByUserId,
        int StudyYearId,
        int DepartmentId,
        int SemesterId,
        CreateSemesterAcademicScheduleDto CreateAcademicScheduleDto
    ) : IRequest<Unit>;
}