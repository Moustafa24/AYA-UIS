using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module.RegistrationDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Commands.Registrations
{
    public class CreateRegistrationCommand : IRequest<Response<RegistrationDto>>
    {
        public CreateRegistrationDto RegistrationDto { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;

        public CreateRegistrationCommand(CreateRegistrationDto registrationDto, string userId)
        {
            RegistrationDto = registrationDto;
            UserId = userId;
        }
    }
}