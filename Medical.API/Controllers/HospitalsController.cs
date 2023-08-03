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

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _hospitalService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await _hospitalService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddSync(AddedHospitalDTO addedHospital)
        {
            var hospitalId = await _hospitalService.AddAsync(addedHospital);
            return Ok(hospitalId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdatedHospitalDTO updatedHospital)
        {
            await _hospitalService.UpdateAsync(updatedHospital);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            await _hospitalService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
