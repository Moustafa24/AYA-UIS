using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AYA_UIS.Application.Commands.AcademicSchedules
{
    public record CreateAcademicScheduleCommand(
        string UploadedByUserId,
        string Title,
        string Description,
        IFormFile File) : IRequest<Unit>;
}