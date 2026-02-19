using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYA_UIS.Application.Commands.StudyYears;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Info_Module.StdudyYearDtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudyYearController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudyYearController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudyYear([FromBody] CreateStudyYearDto studyYearDto)
        {
            var command = new CreateStudyYearCommand(studyYearDto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}