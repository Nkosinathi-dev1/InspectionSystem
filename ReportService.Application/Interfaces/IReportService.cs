using ReportService.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Application.Interfaces
{
    public interface IReportService
    {
        Task<byte[]> GenerateInspectionReportAsync(GenerateReportRequest request);
    }
}
