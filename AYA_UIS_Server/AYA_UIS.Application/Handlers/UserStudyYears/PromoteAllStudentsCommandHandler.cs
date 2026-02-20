using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Application.Commands.UserStudyYears;
using AYA_UIS.Core.Domain.Entities.Identity;
using AYA_UIS.Core.Domain.Entities.Models;
using AYA_UIS.Core.Domain.Enums;
using Domain.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AYA_UIS.Application.Handlers.UserStudyYears
{
    public class PromoteAllStudentsCommandHandler : IRequestHandler<PromoteAllStudentsCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public PromoteAllStudentsCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(PromoteAllStudentsCommand request, CancellationToken cancellationToken)
        {
            var allUngraduatedStudentsIds = await _userManager.Users.Where(u => u.Level != null && u.Level != Levels.Graduate).Select(u => new { u.Id }).ToListAsync();
            if(allUngraduatedStudentsIds.Count == 0)
                throw new Exception("No ungraduated students found");

            var currentStudyYear = await _unitOfWork.StudyYears.GetCurrentStudyYearAsync();
            if (currentStudyYear == null)
                throw new Exception("Current study year not found");


            var usersStudyYears = new List<UserStudyYear>();
            foreach (var id in allUngraduatedStudentsIds)
            {
                usersStudyYears.Add(new UserStudyYear
                {
                    UserId = id.Id,
                    StudyYearId = currentStudyYear!.Id
                });
            }

            await _unitOfWork.UserStudyYears.AddRangeAsync(usersStudyYears);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}