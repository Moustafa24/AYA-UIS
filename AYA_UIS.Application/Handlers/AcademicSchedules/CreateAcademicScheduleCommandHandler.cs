using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Application.Commands.AcademicSchedules;
using CloudinaryDotNet;
using MediatR;
using AYA_UIS.Core.Abstractions.Contracts;

namespace AYA_UIS.Application.Handlers.AcademicSchedules
{
    public class CreateAcademicScheduleCommandHandler : IRequestHandler<CreateAcademicScheduleCommand, Unit>
    {
        private readonly IServiceManager _serviceManager;
        private readonly ICloudinaryService _cloudinaryService;

        public CreateAcademicScheduleCommandHandler(IServiceManager serviceManager, ICloudinaryService cloudinaryService)
        {
            _serviceManager = serviceManager;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Unit> Handle(CreateAcademicScheduleCommand request, CancellationToken cancellationToken)
        {
            var fileId = Guid.NewGuid().ToString();
            var fileUrl = await _cloudinaryService.UploadAcademicScheduleAsync(request.File, fileId, cancellationToken);
            await _serviceManager.AcademicSchedules.AddAsync(request.Title, request.Description, fileId, fileUrl, request.UploadedByUserId);
            return Unit.Value;
        }
    }
}