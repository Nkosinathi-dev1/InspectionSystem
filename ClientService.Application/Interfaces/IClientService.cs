using ClientService.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Interfaces
{
    public interface IClientService
    {
        Task<ClientDto?> GetClientByIdAsync(Guid id);
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();
        Task<Guid> CreateClientAsync(CreateClientDto dto);
        Task UpdateClientAsync(ClientDto dto);
        Task DeleteClientAsync(Guid id);
    }
}
