using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Respones;

namespace AYA_UIS.Application.Commands.Registrations
{
    public class DeleteRegistrationCommand : IRequest<Unit>
    {
        public int RegistrationId { get; set; }

        public DeleteRegistrationCommand(int registrationId)
        {
            RegistrationId = registrationId;
        }
    }
}