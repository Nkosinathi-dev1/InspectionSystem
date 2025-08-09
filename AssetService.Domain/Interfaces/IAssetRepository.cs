using AssetService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetService.Domain.Interfaces
{
    public interface IAssetRepository
    {
        Task<Asset> AddAsync(Asset asset);
        Task<Asset?> GetByIdAsync(Guid id);
        Task<IEnumerable<Asset>> GetAllAsync();
        Task UpdateAsync(Asset asset);
        Task DeleteAsync(Guid id);

        // Inspection history
        Task AddInspectionAsync(InspectionRecord record);
        Task<IEnumerable<InspectionRecord>> GetInspectionHistoryAsync(Guid assetId);

        // query overdue assets
        Task<IEnumerable<Asset>> GetOverdueAssetsAsync(DateTime now);
    }
}
