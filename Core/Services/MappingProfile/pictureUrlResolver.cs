using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.Info_Module;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.Info_Module;

namespace Services.MappingProfile
{
    internal class pictureUrlResolver(IConfiguration _configuration) : IValueResolver<AcademicSchedules, AcademicSchedulesDtos, string>
    {

        public string Resolve(AcademicSchedules source, AcademicSchedulesDtos destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Url)) return string.Empty;
            return $"{_configuration.GetSection("URLS")["BaseUrl"]}{source.Url}";
        }
    }

}
