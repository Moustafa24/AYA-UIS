using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AYA_UIS.Application.Commands.AcademicSchedules
{
    public class UpdateAcademicScheduleCommand : IRequest<Unit>
    {
        public string ScheduleName { get; set; }
        public string FileName { get; set; }
        public IFormFile File { get; set; }

        public UpdateAcademicScheduleCommand(string scheduleName, string fileName, IFormFile file)
        {
            ScheduleName = scheduleName;
            FileName = fileName;
            File = file;
        }
    }
}