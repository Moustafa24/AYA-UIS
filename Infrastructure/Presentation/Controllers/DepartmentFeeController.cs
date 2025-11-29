using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.Info_Module;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentFeesController : ControllerBase
    {
        private readonly IDepartmentFeeService _service;

        public DepartmentFeesController(IDepartmentFeeService service)
        {
            _service = service;
        }

        // GET: api/DepartmentFees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentFeeDtos>>> GetAll()
        {
            var result = await _service.GetAllDepartmentFeeAsync();
            return Ok(result);
        }

        // GET: api/DepartmentFees/{departmentName}/{gradeYear}
        [HttpGet("{departmentName}/{gradeYear}")]
        public async Task<ActionResult<DepartmentFeeDtos>> GetByCompositeKey(string departmentName, string gradeYear)
        {
            var result = await _service.GetDepartmentFeeByCompositeKeyAsync(departmentName, gradeYear);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // PUT: api/DepartmentFees/{departmentName}/{gradeYear}
        [HttpPut("{departmentName}/{gradeYear}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string departmentName, string gradeYear, [FromBody] DepartmentFeeDtos dto)
        {
            await _service.UpdateByCompositeKeyAsync(departmentName, gradeYear, dto);
            return NoContent();
        }

    }


}
