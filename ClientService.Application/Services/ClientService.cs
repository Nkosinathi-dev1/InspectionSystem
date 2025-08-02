using ClientService.Application.Interfaces;
using ClientService.Contracts.DTOs;
using ClientService.Domain.Entities;
using ClientService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<Guid> CreateClientAsync(CreateClientDto dto)
        {
            var client = new Client
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };
            await _clientRepository.AddAsync(client);
            return client.Id;
        }

        public async Task DeleteClientAsync(Guid id)
        {
            await _clientRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.GetAllAsync();
            return clients.Select(c => new ClientDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            });
        }

        public async Task<ClientDto?> GetClientByIdAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null) return null;

            return new ClientDto
            {
                Id = client.Id,
                FullName = client.FullName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            };
        }

        public async Task UpdateClientAsync(ClientDto dto)
        {
            var client = await _clientRepository.GetByIdAsync(dto.Id);
            if (client == null)
                throw new KeyNotFoundException("Client not found");

            client.FullName = dto.FullName;
            client.Email = dto.Email;
            client.PhoneNumber = dto.PhoneNumber;

            await _clientRepository.UpdateAsync(client);
        }
    }
}
