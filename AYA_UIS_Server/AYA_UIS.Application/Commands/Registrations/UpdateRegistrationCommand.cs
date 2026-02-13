using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module.RegistrationDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Commands.Registrations
{
    public class UpdateRegistrationStatusCommand : IRequest<Response<RegistrationDto>>
    {
        public int RegistrationId { get; set; }
        public UpdateRegistrationStatusDto UpdateDto { get; set; } = null!;

        public UpdateRegistrationStatusCommand(int registrationId, UpdateRegistrationStatusDto updateDto)
        {
            RegistrationId = registrationId;
            UpdateDto = updateDto;
        }
    }
}