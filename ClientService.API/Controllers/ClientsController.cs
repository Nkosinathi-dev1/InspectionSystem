using ClientService.Application.Interfaces;
using ClientService.Contracts.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientDto dto)
        {
            var clientId = await _clientService.CreateClientAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = clientId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ClientDto dto)
        {
            if (id != dto.Id) return BadRequest();

            await _clientService.UpdateClientAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _clientService.DeleteClientAsync(id);
            return NoContent();
        }

    }
}
