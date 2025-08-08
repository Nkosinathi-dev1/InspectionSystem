using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionService.Contracts.DTOs
{
    public class CreateInspectionDto
    {
        public string InspectorName { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
