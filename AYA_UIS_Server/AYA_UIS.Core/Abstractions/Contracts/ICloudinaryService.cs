using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AYA_UIS.Core.Abstractions.Contracts
{
    public interface ICloudinaryService
    {
        public Task<string> UploadUserProfilePictureAsync(IFormFile file, string userId, CancellationToken cancellationToken = default);
        public Task<string> UploadAcademicScheduleAsync(IFormFile file, string scheduleId, CancellationToken cancellationToken = default);
        public Task<string> UploadCourseFileAsync(IFormFile file, string fileId, string courseName, CancellationToken cancellationToken = default);
        public Task<bool> DeleteImageAsync(string publicId, CancellationToken cancellationToken = default);
    }
}