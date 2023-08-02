using Medical.BL.DTOs;
using Medical.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medical.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly ILogger<DoctorsController> _logger;
        private readonly IDoctorService _doctorService;

        public DoctorsController(ILogger<DoctorsController> logger, IDoctorService doctorService)
        {
            _logger = logger;
            _doctorService = doctorService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _doctorService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _doctorService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddSync(AddedDoctorDTO addedDoctor)
        {
            await _doctorService.AddAsync(addedDoctor);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdatedDoctorDTO updatedDoctor)
        {
            await _doctorService.UpdateAsync(updatedDoctor);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            await _doctorService.DeleteByIdAsync(id);

            return Ok();
        }
    }
}
