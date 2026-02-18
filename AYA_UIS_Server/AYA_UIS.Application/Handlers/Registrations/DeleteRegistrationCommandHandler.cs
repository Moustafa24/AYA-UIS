using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Application.Commands.Registrations;
using Domain.Contracts;
using MediatR;
using Shared.Respones;

namespace AYA_UIS.Application.Handlers.Registrations
{
    public class DeleteRegistrationCommandHandler : IRequestHandler<DeleteRegistrationCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteRegistrationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Response<bool>> Handle(DeleteRegistrationCommand request, CancellationToken cancellationToken)
        {
            var registration = await _unitOfWork.Registrations.GetByIdAsync(request.RegistrationId);
            if (registration == null)
            {
                return new Response<bool>
                {
                    Success = false,
                    Message = "Registration not found",
                    Data = false
                };
            }

            _unitOfWork.Registrations.Delete(registration);
            await _unitOfWork.SaveChangesAsync();

            return new Response<bool>
            {
                Success = true,
                Message = "Registration deleted successfully",
                Data = true
            };
        }
    }
}