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

        public async Task<Inspection> GetByIdAsync(Guid id)
        {
            return await _context.Inspections.FirstOrDefaultAsync(i => i.Id == id)
               ?? throw new Exception("Inspection not found");
        }
    }
}
