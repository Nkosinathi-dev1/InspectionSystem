using AssetService.Domain.Entities;
using AssetService.Domain.Interfaces;
using AssetService.Persistence.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetService.Persistence.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AssetDbContext _context;
        
        public AssetRepository(AssetDbContext context) => _context=context;

        public async Task<Asset> AddAsync(Asset asset)
        {
            var e = (await _context.Assets.AddAsync(asset)).Entity;
            await _context.SaveChangesAsync();
            return e;
        }

        public async Task AddInspectionAsync(InspectionRecord record)
        {
            await _context.InspectionRecords.AddAsync(record);
            var asset = await _context.Assets.FindAsync(record.AssetId);
            if (asset != null)
            {
                asset.InspectionHistory.Add(record);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var existing = await _context.Assets.FindAsync(id);
            if (existing != null)
            {
                _context.Assets.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Asset>> GetAllAsync() =>
            await _context.Assets.AsNoTracking().ToListAsync();

        public async Task<Asset?> GetByIdAsync(Guid id) =>
            await _context.Assets.Include(a => a.InspectionHistory).FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<InspectionRecord>> GetInspectionHistoryAsync(Guid assetId) =>
            await _context.InspectionRecords
                     .Where(i => i.AssetId == assetId)
                     .OrderByDescending(i => i.InspectionDate)
                     .AsNoTracking()
                     .ToListAsync();

        public async Task<IEnumerable<Asset>> GetOverdueAssetsAsync(DateTime now) =>
            await _context.Assets
                     .Where(a => a.NextInspectionDate.HasValue && a.NextInspectionDate.Value < now)
                     .AsNoTracking()
                     .ToListAsync();

        public async Task UpdateAsync(Asset asset)
        {
            _context.Assets.Update(asset);
            await _context.SaveChangesAsync();
        }
    }
}
