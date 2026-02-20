using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Enums;

namespace Shared.Dtos.Info_Module.FeeDtos
{
    public class UpdateFeeDto
    {
        public FeeType Type { get; set; }
        public Levels Level { get; set; } // first year, second year, etc.
        public string? Description { get; set; }
        public decimal Amount { get; set; }
    }
}