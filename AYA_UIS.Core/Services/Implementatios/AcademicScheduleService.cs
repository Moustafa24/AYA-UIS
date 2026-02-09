using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using AYA_UIS.Core.Abstractions.Contracts;
using AYA_UIS.Core.Domain.Entities.Models;
using Shared.Dtos.Info_Module;
using AYA_UIS.Shared.Exceptions;

namespace AYA_UIS.Core.Services.Implementations
{
    public class AcademicScheduleService : IAcademicSchedulesService
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
       

        public AcademicScheduleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AcademicSchedulesDto>> GetAllAsync()
        {
            var schedules = await _unitOfWork
             .GetRepository<AcademicSchedule, int>()
             .GetAllAsync();
            

            var schedulesDtos = _mapper.Map<IEnumerable<AcademicSchedulesDto>>(schedules);

            if (!schedulesDtos.Any())
                throw new NotFoundException("No academic schedules found.");
            return schedulesDtos;
        }

        public async Task<AcademicSchedulesDto?> GetByNameAsync(string nameScadules)
        {
            var repository = _unitOfWork.GetRepository<AcademicSchedule, int>();

            var allSchedules = await repository.GetAllAsync();

            string cleanedInput = Regex.Replace(nameScadules, @"[^a-zA-Z0-9]", "");

        
            var entity = allSchedules
                .FirstOrDefault(x =>
                    Regex.Replace(x.Title ?? x.Title, @"[^a-zA-Z0-9]", "")
                         .Contains(cleanedInput, StringComparison.OrdinalIgnoreCase));

            if (entity == null)
                throw new NotFoundException($"Academic schedule with name '{nameScadules}' not found.");

            return _mapper.Map<AcademicSchedulesDto>(entity);
        }

        public async Task<int> AddAsync(string title, string description, string fileId, string fileUrl, string uploadedByUserId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new Exception("Title is required.");
           
            var entity = new AcademicSchedule
            {
                Title = title,
                FileId = fileId,
                Url = fileUrl,
                Description = description,
                UploadedByUserId = uploadedByUserId,
                ScheduleDate = DateTime.UtcNow
            };

            var repo = _unitOfWork.GetRepository<AcademicSchedule, int>();
            await repo.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();

            return entity.Id;
        }


        // Delete With Delete Image 
        public async Task<bool> DeleteByNameAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<AcademicSchedule, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException("Shedule not found plaese enter valid id.");

            repo.Delete(entity);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

    }
}
