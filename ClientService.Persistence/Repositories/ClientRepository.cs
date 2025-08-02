using ClientService.Domain.Entities;
using ClientService.Domain.Interfaces;
using ClientService.Persistence.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientDbContext _dbContext;

        public ClientRepository(ClientDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Client client)
        {
            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var client = await _dbContext.Clients.FindAsync(id);
            if (client != null)
            {
                _dbContext.Clients.Remove(client);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Clients.FindAsync(id);
        }

        public async Task UpdateAsync(Client client)
        {
            _dbContext.Clients.Update(client);
            await _dbContext.SaveChangesAsync();
        }
    }
}
