using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.Info_Module;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AcademicSchedulesController : ControllerBase
    {
        #region Old 
        //private readonly IServiceManager _serviceManager;

        //public AcademicSchedulesController(IServiceManager serviceManager)
        //{
        //    _serviceManager = serviceManager;
        //}

        //// GET: api/AcademicSchedules
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<AcademicSchedulesDtos>>> GetAll()
        //{
        //    var schedules = await _serviceManager.AcademicSchedules.GetAllAsync();
        //    return Ok(schedules);
        //}

        //// GET: api/AcademicSchedules/{fileName}
        //[HttpGet("{fileName}")]
        //public async Task<ActionResult<AcademicSchedulesDtos>> GetByName(string fileName)
        //{
        //    var schedule = await _serviceManager.AcademicSchedules.GetByNameAsync(fileName);

        //    if (schedule == null)
        //        return NotFound($"Academic schedule with fileName '{fileName}' not found.");

        //    return Ok(schedule);
        //}

        //// POST: api/AcademicSchedules
        //[HttpPost]
        //public async Task<ActionResult<AcademicSchedulesDtos>> Add([FromBody] AcademicSchedulesDtos dto)
        //{
        //    var createdSchedule = await _serviceManager.AcademicSchedules.AddAsync(dto);
        //    return CreatedAtAction(nameof(GetByName), new { fileName = createdSchedule.Url }, createdSchedule);
        //}

        //// DELETE: api/AcademicSchedules/{fileName}
        //[HttpDelete("{fileName}")]
        //public async Task<ActionResult> Delete(string fileName)
        //{
        //    var deleted = await _serviceManager.AcademicSchedules.DeleteByNameAsync(fileName);

        //    if (!deleted)
        //        return NotFound($"Academic schedule with fileName '{fileName}' not found.");

        //    return NoContent();
        //}

        #endregion


        private readonly IServiceManager _serviceManager;

        public AcademicSchedulesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IEnumerable<AcademicSchedulesDtos>> GetAll()
        {
            return await _serviceManager.AcademicSchedules.GetAllAsync();
        }

        [HttpGet("{fileName}")]
        public async Task<AcademicSchedulesDtos?> GetByName(string fileName)
        {
            return await _serviceManager.AcademicSchedules.GetByNameAsync(fileName);
        }


        #region Old

        //// ===== POST مع رفع صورة =====
        //[HttpPost("upload")]
        //public async Task<AcademicSchedulesDtos> Add([FromForm] AcademicSchedulesDtos dto, IFormFile? file)
        //{
        //    return await _serviceManager.AcademicSchedules.AddAsync(dto, file);
        //}

        #endregion


        [HttpPost("upload")]
        public async Task<IActionResult> Add( string nameScadules, IFormFile file)
        {
            var result = await _serviceManager.AcademicSchedules.AddAsync( nameScadules,  file);
            return Ok(result);
        }



        [HttpDelete("{nameScadules}")]
        public async Task<bool> Delete(string nameScadules)
        {
            return await _serviceManager.AcademicSchedules.DeleteByNameAsync(nameScadules);
        }
    }
}
