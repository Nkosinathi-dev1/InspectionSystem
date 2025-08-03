using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionService.Domain.Entities
{
    public class Inspection
    {
        public Guid Id { get; set; }
        public string InspectorName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
