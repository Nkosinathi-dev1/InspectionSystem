using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportService.Application.Interfaces;
using ReportService.Contracts.Requests;

namespace ReportService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;
        public ReportsController(IReportService reportService)
        {
            _service = reportService;
        }

        [HttpPost("inspection")]
        public async Task<IActionResult> GenerateInspectionReport([FromBody] GenerateReportRequest request)
        {
            var reportBytes = await _service.GenerateInspectionReportAsync(request);
            string contentType = request.Format switch
            {
                ReportFormat.Pdf => "application/pdf",
                ReportFormat.Excel => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ReportFormat.Csv => "text/csv",
                _ => "application/octet-stream"
            };

            string fileExtension = request.Format switch
            {
                ReportFormat.Pdf => "pdf",
                ReportFormat.Excel => "xlsx",
                ReportFormat.Csv => "csv",
                _ => "dat"
            };

            return File(reportBytes, contentType, $"InspectionReport.{fileExtension}");
        }
    }
}
