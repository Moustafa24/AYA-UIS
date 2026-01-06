using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Info_Module;   
using AutoMapper;
using Domain.Contracts;
using Services.Abstraction.Contracts;
using Shared.Dtos.Info_Module;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Services.Implementatios
{
    public class AcademicSchedulesService : IAcademicSchedulesService
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configurationn; 


        public AcademicSchedulesService(IUnitOfWork unitOfWork, IMapper mapper  , IHttpContextAccessor httpContextAccessor , IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _configurationn = configuration;
            _imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(_imagesFolder))
                Directory.CreateDirectory(_imagesFolder);
        }

        private readonly string _imagesFolder = string.Empty;



        public async Task<IEnumerable<AcademicSchedulesDtos>> GetAllAsync()
        {
            var images = await _unitOfWork
             .GetRepository<AcademicSchedules, int>()
             .GetAllAsync();
            

            var Imageresult = _mapper.Map<IEnumerable<AcademicSchedulesDtos>>(images);

            if (!Imageresult.Any())
                throw new AcademicSchedulesNotFoundException("");
            return Imageresult;
        }

        #region Old Get By Name 

        //public async Task<AcademicSchedulesDtos?> GetByNameAsync(string nameScadules)
        //{
        //    var repository = _unitOfWork.GetRepository<Domain.Entities.Info_Module.AcademicSchedules, int>();

        //    var entity = await repository
        //        .FindAsync(x => x.NameScadules.Contains(nameScadules));

        //    if (entity == null) return null;

        //    return _mapper.Map<AcademicSchedulesDtos>(entity);
        //} 
        #endregion
        public async Task<AcademicSchedulesDtos?> GetByNameAsync(string nameScadules)
        {
            var repository = _unitOfWork.GetRepository<AcademicSchedules, int>();

            var allSchedules = await repository.GetAllAsync();

            string cleanedInput = Regex.Replace(nameScadules, @"[^a-zA-Z0-9]", "");

        
            var entity = allSchedules
                .FirstOrDefault(x =>
                    Regex.Replace(x.NameScadules ?? x.FileName, @"[^a-zA-Z0-9]", "")
                         .Contains(cleanedInput, StringComparison.OrdinalIgnoreCase));

            if (entity == null)
                throw new AcademicSchedulesNotFoundException(nameScadules);

            return _mapper.Map<AcademicSchedulesDtos>(entity);
        }

        #region Old
        ////Add With Add Image
        //public async Task<AcademicSchedulesDtos> AddAsync(AcademicSchedulesDtos dto, IFormFile? file = null)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        var savedFileName = $"{Guid.NewGuid()}_{file.FileName}";
        //        var filePath = Path.Combine(_imagesFolder, savedFileName);

        //        using var stream = new FileStream(filePath, FileMode.Create);
        //        await file.CopyToAsync(stream);

        //        dto.Url = $"/images/{savedFileName}";
        //    }

        //    var entity = _mapper.Map<AcademicSchedules>(dto);
        //    var repo = _unitOfWork.GetRepository<AcademicSchedules, int>();
        //    await repo.AddAsync(entity);
        //    await _unitOfWork.SaveChangeAsync();

        //    return _mapper.Map<AcademicSchedulesDtos>(entity);
        //} 
        #endregion

        public async Task<AcademicSchedulesDtos> AddAsync( string NameScadules,  IFormFile file)
        {
            if (string.IsNullOrWhiteSpace(NameScadules))
                throw new Exception("NameScadules is required.");

          
            if (file == null || file.Length == 0)
                throw new Exception("File is required");


            var savedFileName = $"{Guid.NewGuid()}_{file.FileName}";

            var filePath = Path.Combine(_imagesFolder, savedFileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

           
            var entity = new AcademicSchedules
            {
               NameScadules = NameScadules,
                FileName = savedFileName,
                Url = $"/images/{savedFileName}"
            };

            var repo = _unitOfWork.GetRepository<AcademicSchedules, int>();
            await repo.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();

            return new AcademicSchedulesDtos
            {
                Id = entity.Id,
                NameScadules = NameScadules,
                FileName = entity.FileName,
                Url = entity.Url
            };
        }


        // Delete With Delete Image 
        public async Task<bool> DeleteByNameAsync(string nameScadules)
        {
            var repo = _unitOfWork.GetRepository<AcademicSchedules, int>();
            var entity = await repo.FindAsync(x => x.NameScadules == nameScadules);
            if (entity == null)
                throw new AcademicSchedulesNotFoundException(nameScadules);



            if (!string.IsNullOrEmpty(entity.Url))
            {
                var fullPath = Path.Combine(_imagesFolder, Path.GetFileName(entity.Url));
                if (File.Exists(fullPath))
                    File.Delete(fullPath);
            }

            repo.Delete(entity);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

    }
}
