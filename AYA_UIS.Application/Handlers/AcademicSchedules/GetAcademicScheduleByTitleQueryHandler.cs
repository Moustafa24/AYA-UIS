using AYA_UIS.Application.Queries.AcademicSchedules;
using AutoMapper;
using AYA_UIS.Shared.Exceptions;
using Domain.Contracts;
using MediatR;
using Shared.Dtos.Info_Module.AcademicSheduleDtos;

namespace AYA_UIS.Application.Handlers.AcademicSchedules
{
    public class GetAcademicScheduleByTitleQueryHandler : IRequestHandler<GetAcademicScheduleByTitleQuery, AcademicSchedulesDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAcademicScheduleByTitleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AcademicSchedulesDto> Handle(GetAcademicScheduleByTitleQuery request, CancellationToken cancellationToken)
        {
            var schedule = await _unitOfWork.AcademicSchedules.GetByTitleAsync(request.ScheduleTitle);

            if (schedule is null)
                throw new NotFoundException($"Academic schedule with title '{request.ScheduleTitle}' not found.");

            return _mapper.Map<AcademicSchedulesDto>(schedule);
        }
    }
}
