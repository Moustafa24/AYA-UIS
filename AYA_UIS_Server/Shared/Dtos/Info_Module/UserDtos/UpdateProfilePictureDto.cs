using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shared.Dtos.Info_Module.UserDtos
{
    public class UpdateProfilePictureDto
    {
        public IFormFile ProfilePicture { get; set; }
    }
}