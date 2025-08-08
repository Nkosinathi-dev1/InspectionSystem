using InspectionService.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionService.Application.Interfaces
{
    public interface IInspectionService
    {
        Task<InspectionDto?> GetInspectionAsync(Guid id);
        Task<IEnumerable<InspectionDto>> GetAllInspectionAsync();
        Task<Guid> CreateInspectionAsync(CreateInspectionDto dto);
        Task UpdateInspectionAsync(InspectionDto dto);
        Task DeleteInspectionAsync(Guid id);
    }
}
