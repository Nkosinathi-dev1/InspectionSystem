using InspectionService.Application.Interfaces;
using InspectionService.Contracts.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InspectionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionController : ControllerBase
    {
        private readonly IInspectionService _inspectionService;

        public InspectionController(IInspectionService inspectionService)
        {
            _inspectionService = inspectionService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _inspectionService.GetInspectionAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _inspectionService.GetAllInspectionAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInspectionDto dto)
        {
            var clientId = await _inspectionService.CreateInspectionAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = clientId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, InspectionDto dto)
        {
            if (id != dto.Id) return BadRequest();

            await _inspectionService.UpdateInspectionAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _inspectionService.DeleteInspectionAsync(id);
            return NoContent();
        }

    }
}
