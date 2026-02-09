using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AYA_UIS.Application.Commands.AcademicSchedules;
using AYA_UIS.Application.Queries;
using AYA_UIS.Application.Queries.AcademicSchedules;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Info_Module;

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
        public async Task<IEnumerable<AcademicSchedulesDto>> GetAll()
        {
            var query = new GetAllAcademicSchedulesQuery();
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("{scheduleTitle}")]
        public async Task<AcademicSchedulesDto?> GetByName(string scheduleTitle)
        {
            var query = new GetAcademicScheduleByTitleQuery(scheduleTitle);
            var result = await _mediator.Send(query);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(string title, IFormFile file)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var command = new CreateAcademicScheduleCommand(userId, title, file.FileName, file);
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(int Id, string scheduleName, IFormFile file)
        {
            var command = new UpdateAcademicScheduleCommand(scheduleName, file.FileName, file);
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{scheduleTitle}")]
        public async Task<bool> Delete(string scheduleTitle)
        {
            var command = new DeleteAcademicScheduleByTitleCommand(scheduleTitle);
            var result = await _mediator.Send(command);
            return result;
        }
    }
}
