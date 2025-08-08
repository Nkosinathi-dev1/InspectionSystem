using InspectionService.Application.Interfaces;
using InspectionService.Contracts.DTOs;
using InspectionService.Domain.Entities;
using InspectionService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionService.Application.Services
{
    public class InspectionService : IInspectionService
    {
        private readonly IInspectionRepository _repo;
        public InspectionService(IInspectionRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> CreateInspectionAsync(CreateInspectionDto dto)
        {
            var inspiration = new Inspection
            {
                InspectorName = dto.InspectorName,
                Date = dto.Date
            };
            await _repo.AddAsync(inspiration);
            return inspiration.Id;
        }

        public async Task DeleteInspectionAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<InspectionDto>> GetAllInspectionAsync()
        {
            var inspiration = await _repo.GetAllAsync();
            return inspiration.Select(i => new InspectionDto
            {
                Id = i.Id,
                InspectorName = i.InspectorName,
                Date = i.Date
            });
        }

        public async Task<InspectionDto?> GetInspectionAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return new InspectionDto
            {
                Id = entity.Id,
                InspectorName = entity.InspectorName,
                Date = entity.Date
            };
        }

        public async Task UpdateInspectionAsync(InspectionDto dto)
        {
            var inspiration = await _repo.GetByIdAsync(dto.Id);
            if (inspiration == null)
                throw new KeyNotFoundException("Client not found");
            
            inspiration.InspectorName = dto.InspectorName;

            await _repo.UpdateAsync(inspiration);
        }
    }
}
