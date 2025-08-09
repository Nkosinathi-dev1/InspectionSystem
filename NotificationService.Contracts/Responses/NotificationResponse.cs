using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Contracts.Responses
{
    public class NotificationResponse
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
    }
}
