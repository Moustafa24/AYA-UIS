using AutoMapper;
using AYA_UIS.Core.Domain.Entities.Models;
using Shared.Dtos.Info_Module.AcademicSheduleDtos;
using Shared.Dtos.Info_Module.CourseDtos;
using Shared.Dtos.Info_Module.CourseUploadDtos;
using Shared.Dtos.Info_Module.DepartmentDtos;
using Shared.Dtos.Info_Module.DepartmentFeeDtos;
using Shared.Dtos.Info_Module.FeeDtos;

namespace AYA_UIS.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Department mappings
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Department, DepartmentDetailsDto>().ReverseMap();
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();

            // AcademicSchedule mappings
            CreateMap<AcademicSchedule, AcademicSchedulesDto>().ReverseMap();
            CreateMap<CreateAcademicScheduleDto, AcademicSchedule>();

            // DepartmentFee mappings
            CreateMap<DepartmentFee, DepartmentFeeDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Department.Name))
                .ForMember(d => d.StartYear, opt => opt.MapFrom(s => s.StudyYear.StartYear))
                .ForMember(d => d.EndYear, opt => opt.MapFrom(s => s.StudyYear.EndYear));

            // Fee mappings
            CreateMap<Fee, FeeDto>().ReverseMap();

            //Course mappings
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<CreateCourseDto, Course>();
            //CreateMap<UpdateCourseDto, Course>();

            //CourseUpload mappings
            CreateMap<CourseUpload, CourseUploadDto>().ReverseMap();
            CreateMap<CreateCourseUploadDto, CourseUpload>();
        }
    }
}
