using AYA_UIS.Application.Commands.AcademicSchedules;
using AYA_UIS.Application.Contracts;
using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using MediatR;

namespace AYA_UIS.Application.Handlers.AcademicSchedules
{
    public class CreateAcademicScheduleCommandHandler : IRequestHandler<CreateAcademicScheduleCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;

        public CreateAcademicScheduleCommandHandler(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Unit> Handle(CreateAcademicScheduleCommand request, CancellationToken cancellationToken)
        {
            var fileId = Guid.NewGuid().ToString();
            var fileUrl = await _cloudinaryService.UploadAcademicScheduleAsync(request.File, fileId, cancellationToken);

            var entity = new AcademicSchedule
            {
                Title = request.Title,
                FileId = fileId,
                Url = fileUrl,
                Description = request.Description,
                UploadedByUserId = request.UploadedByUserId,
                ScheduleDate = DateTime.UtcNow
            };

            await _unitOfWork.AcademicSchedules.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}