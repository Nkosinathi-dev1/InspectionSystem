using ClosedXML.Excel;
using CsvHelper;
using ReportService.Application.Interfaces;
using ReportService.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace ReportService.Infrastructure.Services
{
    public class ReportServiceImpl : IReportService
    {
        public async Task<byte[]> GenerateInspectionReportAsync(GenerateReportRequest request)
        {
            // I am using this dummy inspection data for testing demo
            var data = GetDummyInspectionData(request);

            return request.Format switch
            {
                ReportFormat.Pdf => GeneratePdfReport(data),
                ReportFormat.Excel => GenerateExcelReport(data),
                ReportFormat.Csv => GenerateCsvReport(data),
                _ => throw new NotSupportedException("Format not supported")
            };
        }

        private List<(string Client, DateTime Date, string Findings, string Compliance)> GetDummyInspectionData(GenerateReportRequest request)
        {
            return new List<(string, DateTime, string, string)>
            {
                ("Client A", DateTime.UtcNow.AddDays(-10), "All good", "Compliant"),
                ("Client A", DateTime.UtcNow.AddDays(-5), "Minor issues", "Non-compliant"),
                ("Client B", DateTime.UtcNow.AddDays(-3), "Pending review", "Unknown")
            };
        }

        private byte[] GeneratePdfReport(List<(string Client, DateTime Date, string Findings, string Compliance)> data)
        {
            using var ms = new MemoryStream();

            QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text("Inspection Report")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Client");
                                header.Cell().Text("Date");
                                header.Cell().Text("Findings");
                                header.Cell().Text("Compliance");
                            });

                            foreach (var row in data)
                            {
                                table.Cell().Text(row.Client);
                                table.Cell().Text(row.Date.ToShortDateString());
                                table.Cell().Text(row.Findings);
                                table.Cell().Text(row.Compliance);
                            }
                        });
                });
            }).GeneratePdf(ms);

            return ms.ToArray();
        }

        private byte[] GenerateExcelReport(List<(string Client, DateTime Date, string Findings, string Compliance)> data)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Inspection Report");

            worksheet.Cell(1, 1).Value = "Client";
            worksheet.Cell(1, 2).Value = "Date";
            worksheet.Cell(1, 3).Value = "Findings";
            worksheet.Cell(1, 4).Value = "Compliance";

            int row = 2;
            foreach (var item in data)
            {
                worksheet.Cell(row, 1).Value = item.Client;
                worksheet.Cell(row, 2).Value = item.Date;
                worksheet.Cell(row, 3).Value = item.Findings;
                worksheet.Cell(row, 4).Value = item.Compliance;
                row++;
            }

            using var ms = new MemoryStream();
            workbook.SaveAs(ms);
            return ms.ToArray();
        }

        private byte[] GenerateCsvReport(List<(string Client, DateTime Date, string Findings, string Compliance)> data)
        {
            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms, Encoding.UTF8, leaveOpen: true);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteField("Client");
            csv.WriteField("Date");
            csv.WriteField("Findings");
            csv.WriteField("Compliance");
            csv.NextRecord();

            foreach (var item in data)
            {
                csv.WriteField(item.Client);
                csv.WriteField(item.Date.ToString("o")); // ISO8601
                csv.WriteField(item.Findings);
                csv.WriteField(item.Compliance);
                csv.NextRecord();
            }

            writer.Flush();
            return ms.ToArray();
        }

    }
}
