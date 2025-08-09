using AssetService.Application.Interfaces;
using AssetService.Contracts.Requests;
using AssetService.Contracts.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _service;

        public AssetsController(IAssetService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterAssetRequest request)
        {
            var id = await _service.RegisterAssetAsync(request);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id}/link-client")]
        public async Task<IActionResult> LinkClient(Guid id, [FromBody] Guid clientId)
        {
            await _service.LinkAssetToClientAsync(id, clientId);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var asset = await _service.GetAssetByIdAsync(id);
            if (asset == null) return NotFound();
            return Ok(asset);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAssetsAsync();
            return Ok(list);
        }

        [HttpGet("{id}/inspection-history")]
        public async Task<IActionResult> InspectionHistory(Guid id)
        {
            var history = await _service.GetInspectionHistoryAsync(id);
            return Ok(history);
        }

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdue()
        {
            var list = await _service.GetOverdueAssetsAsync();
            return Ok(list);
        }

        // Optional: add inspection record
        [HttpPost("{id}/inspections")]
        public async Task<IActionResult> AddInspection(Guid id, [FromBody] InspectionRecordDto dto)
        {
            await _service.AddInspectionRecordAsync(id, dto.InspectorName, dto.Status, dto.Notes, dto.InspectionDate);
            return NoContent();
        }
    }
}
