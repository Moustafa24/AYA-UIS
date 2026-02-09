using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AYA_UIS.Application.Commands.AcademicSchedules;
using AYA_UIS.Application.Queries.AcademicSchedules;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Info_Module.AcademicSheduleDtos;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AcademicSchedulesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AcademicSchedulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllAcademicSchedulesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var query = new GetAcademicScheduleByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{scheduleTitle}")]
        public async Task<IActionResult> GetByTitle([FromRoute]string scheduleTitle)
        {
            var query = new GetAcademicScheduleByTitleQuery(scheduleTitle);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateAcademicScheduleCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UpdateAcademicScheduleCommand command)
        {

            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]DeleteAcademicScheduleByIdCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
