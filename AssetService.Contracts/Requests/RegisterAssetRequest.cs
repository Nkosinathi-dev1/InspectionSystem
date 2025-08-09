using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetService.Contracts.Requests
{
    public class RegisterAssetRequest
    {
        public string AssetType { get; set; } 
        public string Identifier { get; set; } 
        public string Description { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
