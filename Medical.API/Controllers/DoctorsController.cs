using Medical.BL.DTOs;
using Medical.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medical.API.Controllers
{
    // Base route for the controller
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        // Constructor injection for the IDoctorService
        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // HTTP GET: api/v1/Doctors
        // Get all doctors
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _doctorService.GetAllAsync());
        }

        // HTTP GET: api/v1/Doctors/{id}
        // Get a doctor by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _doctorService.GetByIdAsync(id));
        }

        // HTTP POST: api/v1/Doctors
        // Add a new doctor
        [HttpPost]
        public async Task<IActionResult> AddSync(AddedDoctorDTO addedDoctor)
        {
            await _doctorService.AddAsync(addedDoctor);
            return Ok();
        }

        // HTTP PUT: api/v1/Doctors
        // Update an existing doctor
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdatedDoctorDTO updatedDoctor)
        {
            await _doctorService.UpdateAsync(updatedDoctor);
            return Ok();
        }

        // HTTP DELETE: api/v1/Doctors/{id}
        // Delete a doctor by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            await _doctorService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
