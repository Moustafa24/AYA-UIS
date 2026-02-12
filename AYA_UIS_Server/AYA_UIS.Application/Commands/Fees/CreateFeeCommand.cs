using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
namespace AYA_UIS.Application.Commands.Fees
{
    public record CreateFeeCommand : IRequest<int>
    {
        public int DepartmentFeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}