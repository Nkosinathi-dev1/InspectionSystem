using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetService.Domain.Entities
{
    public class InspectionRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AssetId { get; set; }
        public DateTime InspectionDate { get; set; } = DateTime.UtcNow;
        public string InspectorName { get; set; } = null!;
        public string Status { get; set; } = null!; // Passed, Failed, Pending
        public string? Notes { get; set; }

        public Asset? Asset { get; set; }
    }
}
