using Medical.BL.DTOs;
using Medical.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medical.API.Controllers
{
    // Base route for the controller
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;

        public HospitalsController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        // HTTP GET: api/v1/hospitals
        // Get all hospitals
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _hospitalService.GetAllAsync());
        }

        // HTTP GET: api/v1/hospitals/{id}
        // Get a hospital by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await _hospitalService.GetByIdAsync(id));
        }

        // HTTP POST: api/v1/hospitals
        // Add a new hospital
        // returns the guid id of created entity
        [HttpPost]
        public async Task<IActionResult> AddSync(AddedHospitalDTO addedHospital)
        {
            var hospitalId = await _hospitalService.AddAsync(addedHospital);
            return Ok(hospitalId);
        }

        // HTTP PUT: api/v1/hospitals
        // Update an existing hospital
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdatedHospitalDTO updatedHospital)
        {
            await _hospitalService.UpdateAsync(updatedHospital);
            return Ok();
        }

        // HTTP DELETE: api/v1/hospitals/{id}
        // Delete a hospital by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            await _hospitalService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
