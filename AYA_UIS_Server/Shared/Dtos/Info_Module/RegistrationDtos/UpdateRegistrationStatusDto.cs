using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Enums;

namespace Shared.Dtos.Info_Module.RegistrationDtos
{
    public class UpdateRegistrationStatusDto
    {
        public Statuses Status { get; set; }
        public string? Reason { get; set; }
        public Grads? Grade { get; set; }
    }
}