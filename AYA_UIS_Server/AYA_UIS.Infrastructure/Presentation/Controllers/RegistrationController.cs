using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AYA_UIS.Application.Commands.Registrations;
using AYA_UIS.Application.Queries.Registrations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Info_Module.RegistrationDtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RegistrationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterForCourse(CreateRegistrationDto registrationDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var command = new CreateRegistrationCommand(registrationDto, userId);
            var result = await _mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("my-registrations")]
        public async Task<IActionResult> GetMyRegistrations(int? studyYearId = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var query = new GetUserRegistrationsQuery(userId, studyYearId);
            var result = await _mediator.Send(query);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> GetAllRegistrations(int? courseId = null, int? studyYearId = null, int? semesterId = null, string? userId = null)
        {
            var query = new GetAllRegistrationsQuery(courseId, studyYearId, semesterId, userId);
            var result = await _mediator.Send(query);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{registrationId}/status")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> UpdateRegistrationStatus(int registrationId, UpdateRegistrationStatusDto updateDto)
        {
            var command = new UpdateRegistrationStatusCommand(registrationId, updateDto);
            var result = await _mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}