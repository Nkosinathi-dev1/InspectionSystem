using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Interfaces;
using NotificationService.Contracts.Requests;

namespace NotificationService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationsController(INotificationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] SendNotificationRequest request)
        {
            var result = await _service.SendNotificationAsync(request);
            return Ok(result);
        }
    }
}
