using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.Info_Module;
using Shared.Dtos.Info_Module;

namespace Services.MappingProfile
{
    public class _InfoMappingProfile : Profile
    {
        public _InfoMappingProfile()
        {
            // DTO -> Entity (للتحديث)
            CreateMap<DepartmentFeeDtos, DepartmentFee>()
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.GradeYear, opt => opt.Ignore());

            // Entity -> DTO (للعرض)
            CreateMap<DepartmentFee, DepartmentFeeDtos>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.GradeYear, opt => opt.MapFrom(src => src.GradeYear.Name));

            CreateMap<AcademicSchedules, AcademicSchedulesDtos>()
                .ForMember(dest => dest.Url
                , option => option.MapFrom<pictureUrlResolver>()).ReverseMap(); 


        }

    }
 }

