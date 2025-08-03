using InspectionService.Application.Interfaces;
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
        public async Task<IActionResult> GetInspection(Guid id)
        {
            var result = await _inspectionService.GetInspectionAsync(id);
            return Ok(result);
        }
    }
}
