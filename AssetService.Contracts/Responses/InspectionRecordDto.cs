using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetService.Contracts.Responses
{
    public class InspectionRecordDto
    {
        public DateTime InspectionDate { get; set; }
        public string InspectorName { get; set; }
        public string Status { get; set; } // Passed, Failed, or Pending
        public string Notes { get; set; }
    }
}
