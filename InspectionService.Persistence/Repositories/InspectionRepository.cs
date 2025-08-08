using InspectionService.Domain.Entities;
using InspectionService.Domain.Interfaces;
using InspectionService.Persistence.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionService.Persistence.Repositories
{
    public class InspectionRepository : IInspectionRepository
    {
        private readonly InspectionDbContext _context;

        public InspectionRepository(InspectionDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Inspection inspection)
        {
            await _context.Inspections.AddAsync(inspection);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var inspection = await _context.Inspections.FindAsync(id);
            if (inspection != null)
            {
                _context.Inspections.Remove(inspection);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Inspection>> GetAllAsync()
        {
            return await _context.Inspections.ToListAsync();
        }

        public async Task<Inspection?> GetByIdAsync(Guid id)
        {
            return await _context.Inspections.FirstOrDefaultAsync(i => i.Id == id);
               
        }

        public async Task UpdateAsync(Inspection inspection)
        {
            _context.Inspections.Update(inspection);
            await _context.SaveChangesAsync();
        }
    }
}
