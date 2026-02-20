using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Dtos.Info_Module.FeeDtos;

namespace AYA_UIS.Application.Commands.Fees
{
    public class UpdateFeeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public UpdateFeeDto FeeDto { get; set; }

        public UpdateFeeCommand(int id, UpdateFeeDto feeDto)
        {
            Id = id;
            FeeDto = feeDto;
        }
    }
}