using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetService.Domain.Entities
{
    public class Asset
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string AssetType { get; set; } = null!;          // Vehicle, Machine, etc.
        public string Identifier { get; set; } = null!;         // VIN, Serial No.
        public string? Description { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        // Optional relationship to client
        public Guid? ClientId { get; set; }

        public DateTime? NextInspectionDate { get; set; }

        public List<InspectionRecord> InspectionHistory { get; set; } = new();
    }
}
