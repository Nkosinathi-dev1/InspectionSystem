using InspectionService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionService.Domain.Interfaces
{
    public interface IInspectionRepository
    {
        Task<Inspection> GetByIdAsync(Guid id);
    }
}
