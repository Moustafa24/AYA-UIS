using AYA_UIS.Application.Queries.AcademicSchedules;
using AutoMapper;
using AYA_UIS.Shared.Exceptions;
using Domain.Contracts;
using MediatR;
using Shared.Dtos.Info_Module.AcademicSheduleDtos;

namespace AYA_UIS.Application.Handlers.AcademicSchedules
{
    public class GetAllAcademicSchedulesQueryHandler : IRequestHandler<GetAllAcademicSchedulesQuery, List<AcademicSchedulesDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAcademicSchedulesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AcademicSchedulesDto>> Handle(GetAllAcademicSchedulesQuery request, CancellationToken cancellationToken)
        {
            var schedules = await _unitOfWork.AcademicSchedules.GetAllWithDetailsAsync();

            var result = _mapper.Map<List<AcademicSchedulesDto>>(schedules);

            return result;
        }
    }
}
