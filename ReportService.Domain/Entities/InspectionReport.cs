using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Entities
{
    public class InspectionReport
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public DateTime InspectionDate { get; set; }
        public string Findings { get; set; } = string.Empty;
        public string ComplianceStatus { get; set; } = string.Empty;
        // TODO: I will add more domain-specific properties to required on a report...
    }
}
