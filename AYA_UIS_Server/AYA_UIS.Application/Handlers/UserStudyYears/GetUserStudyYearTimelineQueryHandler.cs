using AutoMapper;
using AYA_UIS.Application.Queries.UserStudyYears;
using AYA_UIS.Core.Domain.Entities.Identity;
using AYA_UIS.Core.Domain.Entities.Models;
using AYA_UIS.Core.Domain.Enums;
using Domain.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos.Info_Module.StudyYearDtos;
using Shared.Dtos.Info_Module.UserStudyYearDtos;
using Shared.Respones;

namespace AYA_UIS.Application.Handlers.UserStudyYears
{
    public class GetUserStudyYearTimelineQueryHandler : IRequestHandler<GetUserStudyYearTimelineQuery, Response<UserStudyYearTimelineDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetUserStudyYearTimelineQueryHandler(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response<UserStudyYearTimelineDto>> Handle(GetUserStudyYearTimelineQuery request, CancellationToken cancellationToken)
        {
            //get user
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            if(user == null)
                return Response<UserStudyYearTimelineDto>.ErrorResponse("User not found");
            //get all study years related to user ordered by start year
            var userStudyYears = await _unitOfWork.UserStudyYears.GetStudyYearsByUserIdAsync(request.UserId);

            if (!userStudyYears.Any())
                return Response<UserStudyYearTimelineDto>.ErrorResponse("No study year records found for this user.");
            
            //then get department related to his study years
            if (user.DepartmentId == null)
                return Response<UserStudyYearTimelineDto>.ErrorResponse("Department not found for the user.");

            var department = await _unitOfWork.Departments.GetByIdAsync(user.DepartmentId.Value);
            if(department == null)
                return Response<UserStudyYearTimelineDto>.ErrorResponse("Department not found for the user.");

            // get total completed years (non current)
            var completedYears = userStudyYears.Where(sy => !sy.IsCurrent).ToList();
            var timeline = new UserStudyYearTimelineDto
            {
                UserId = request.UserId,
                CurrentLevel = user.Level,
                TotalYearsCompleted = completedYears.Count,
                IsGraduated = user.Level == Levels.Graduate,
                DepartmentName = department.Name,
                StudyYears =  userStudyYears.Select(sy => new UserStudyYearDetailsDto
                {
                    UserStudyYearId = sy.Id,
                    StartYear = sy.StudyYear.StartYear,
                    EndYear = sy.StudyYear.EndYear,
                    Level = sy.Level,
                    IsActive = sy.IsActive,
                    IsCurrent = sy.StudyYear.IsCurrent,
                    EnrolledAt = sy.EnrolledAt
                }).OrderByDescending(sy => sy.StartYear).ToList()
            };

            Console.WriteLine(timeline);

            return Response<UserStudyYearTimelineDto>.SuccessResponse(timeline);
        }
    }
}
