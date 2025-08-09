using AssetService.Application.Interfaces;
using AssetService.Contracts.Requests;
using AssetService.Contracts.Responses;
using AssetService.Domain.Entities;
using AssetService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetService.Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _repo;
        public AssetService(IAssetRepository repo) => _repo = repo;

        public async Task AddInspectionRecordAsync(Guid assetId, string inspectorName, string status, string? notes = null, DateTime? inspectionDate = null)
        {
            var rec = new InspectionRecord
            {
                AssetId = assetId,
                InspectorName = inspectorName,
                Status = status,
                Notes = notes,
                InspectionDate = inspectionDate ?? DateTime.UtcNow
            };
            await _repo.AddInspectionAsync(rec);
        }

        public async Task<IEnumerable<AssetResponse>> GetAllAssetsAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(MapToResponse);
        }

        public async Task<AssetResponse?> GetAssetByIdAsync(Guid id)
        {
            var a = await _repo.GetByIdAsync(id);
            if (a == null) return null;
            return MapToResponse(a);
        }

        public async Task<InspectionHistoryResponse> GetInspectionHistoryAsync(Guid assetId)
        {
            var asset = await _repo.GetByIdAsync(assetId)
                    ?? throw new KeyNotFoundException("Asset not found");

            var records = await _repo.GetInspectionHistoryAsync(assetId);

            var dto = new InspectionHistoryResponse
            {
                AssetId = assetId,
                AssetIdentifier = asset.Identifier,
                Inspections = records.Select(r => new InspectionRecordDto
                {
                    InspectionDate = r.InspectionDate,
                    InspectorName = r.InspectorName,
                    Status = r.Status,
                    Notes = r.Notes
                }).ToList()
            };
            return dto;
        }

        public async Task<IEnumerable<AssetResponse>> GetOverdueAssetsAsync()
        {
            var list = await _repo.GetOverdueAssetsAsync(DateTime.UtcNow);
            return list.Select(MapToResponse);
        }

        public async Task LinkAssetToClientAsync(Guid assetId, Guid clientId)
        {
            var asset = await _repo.GetByIdAsync(assetId)
                    ?? throw new KeyNotFoundException("Asset not found");

            asset.ClientId = clientId;
            await _repo.UpdateAsync(asset);
        }

        public async Task<Guid> RegisterAssetAsync(RegisterAssetRequest request)
        {
            var asset = new Asset
            {
                AssetType = request.AssetType,
                Identifier = request.Identifier,
                Description = request.Description,
                RegistrationDate = request.RegistrationDate
            };

            var created = await _repo.AddAsync(asset);
            return created.Id;
        }

        private static AssetResponse MapToResponse(Asset a) =>
        new AssetResponse
        {
            Id = a.Id,
            AssetType = a.AssetType,
            Identifier = a.Identifier,
            Description = a.Description ?? string.Empty,
            RegistrationDate = a.RegistrationDate,
            IsInspectionOverdue = a.NextInspectionDate.HasValue && a.NextInspectionDate.Value < DateTime.UtcNow
        };
    }
}
