using InspectionService.Application.Interfaces;
using InspectionService.Contracts.DTOs;
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

        public async Task<InspectionDto> GetInspectionAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return new InspectionDto
            {
                Id = entity.Id,
                InspectorName = entity.InspectorName,
                Date = entity.Date
            };
        }
    }
}
