using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetService.Contracts.Responses
{
    public class InspectionHistoryResponse
    {
        public Guid AssetId { get; set; }
        public string AssetIdentifier { get; set; }
        public List<InspectionRecordDto> Inspections { get; set; } = new();
    }
}
