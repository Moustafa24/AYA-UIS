using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Auth_Module
{
    public record RegisterDto
    {
        //public string FullName { get; set; } = string.Empty;
        //public string Email { get; set; } = string.Empty;
        //public string Password { get; set; } = string.Empty;
        //public string Role { get; set; } = string.Empty; // Admin / Instructor / Student
        //public string UniversityCode { get; set; } = string.Empty;


        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        [Phone]
        
        public string? PhoneNumber { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Role { get; set; } =  "Student";
        public string Academic_Code { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;

    }
}
