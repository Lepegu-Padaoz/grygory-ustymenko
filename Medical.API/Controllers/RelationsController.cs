using Medical.BL.DTOs;
using Medical.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medical.API.Controllers
{
    // Base route for the controller
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RelationsController : ControllerBase
    {
        private readonly IRelationService _relationService;
        public RelationsController(IRelationService relationService)
        {
            _relationService = relationService;
        }

        [HttpPost("{hospitalId}/doctors/{doctorId}")]
        public async Task<IActionResult> LinkAsync(Guid hospitalId, int doctorId)
        {
            await _relationService.LinkAsync(hospitalId, doctorId);

            return Ok();
        }

        [HttpGet("{hospitalId}/doctors")]
        public async Task<IActionResult> GetDoctorsByHospitalIdAsync(Guid hospitalId)
        {
            var doctors = await _relationService.GetDoctorsByHospitalIdAsync(hospitalId);

            return Ok(doctors);
        }
    }
}
