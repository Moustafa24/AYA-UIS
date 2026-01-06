using AYA_UIS.Application.Commands.DepartmentFees;
using AYA_UIS.Application.Queries.DepartmentFees;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Info_Module;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentFeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentFeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/DepartmentFees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentFeeDtos>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllDepartmentFeesQuery());
            return Ok(result);
        }

        // GET: api/DepartmentFees/{departmentName}/{gradeYear}
        [HttpGet("{departmentName}/{gradeYear}")]
        public async Task<ActionResult<DepartmentFeeDtos>> GetByCompositeKey(string departmentName, string gradeYear)
        {
            var result = await _mediator.Send(new GetDepartmentFeeByCompositeKeyQuery(departmentName, gradeYear));
            if (result == null) return NotFound();
            return Ok(result);
        }

        // PUT: api/DepartmentFees/{departmentName}/{gradeYear}
        [HttpPut("{departmentName}/{gradeYear}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string departmentName, string gradeYear, [FromBody] DepartmentFeeDtos dto)
        {
            await _mediator.Send(new UpdateDepartmentFeeCommand(departmentName, gradeYear, dto));
            return NoContent();
        }

    }


}
