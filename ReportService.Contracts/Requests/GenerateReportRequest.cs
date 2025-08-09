using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Contracts.Requests
{
    public class GenerateReportRequest
    {
        public string ClientName { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ReportFormat Format { get; set; } = ReportFormat.Pdf;
    }
}
