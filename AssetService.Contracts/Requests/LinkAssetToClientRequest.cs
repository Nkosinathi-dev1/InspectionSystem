using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetService.Contracts.Requests
{
    public class LinkAssetToClientRequest
    {
        public Guid AssetId { get; set; }
        public Guid ClientId { get; set; }
    }
}
