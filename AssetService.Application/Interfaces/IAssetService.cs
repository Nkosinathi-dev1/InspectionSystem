using AssetService.Contracts.Requests;
using AssetService.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetService.Application.Interfaces
{
    public interface IAssetService
    {
        Task<Guid> RegisterAssetAsync(RegisterAssetRequest request);
        Task LinkAssetToClientAsync(Guid assetId, Guid clientId);
        Task<AssetResponse?> GetAssetByIdAsync(Guid id);
        Task<IEnumerable<AssetResponse>> GetAllAssetsAsync();
        Task<IEnumerable<AssetResponse>> GetOverdueAssetsAsync();
        Task<InspectionHistoryResponse> GetInspectionHistoryAsync(Guid assetId);

        // ?? add method to add inspection record
        Task AddInspectionRecordAsync(Guid assetId, string inspectorName, string status, string? notes = null, DateTime? inspectionDate = null);
    }
}
