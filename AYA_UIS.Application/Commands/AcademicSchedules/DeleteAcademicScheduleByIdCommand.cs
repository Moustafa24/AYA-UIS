using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace AYA_UIS.Application.Commands.AcademicSchedules
{
    public class DeleteAcademicScheduleByIdCommand : IRequest<bool>
    {
        public int Id { get; init; }

        public DeleteAcademicScheduleByIdCommand(int id)
        {
            Id = id;
        }
    }
}