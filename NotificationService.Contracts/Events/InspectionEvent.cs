using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Contracts.Events
{
    //I will use this with message broker RabbitMQ  maybe I will try Kafka later
    public class InspectionEvent
    {
        public string EventType { get; set; } = string.Empty; // "FailedInspection", "NewAssignment", etc.
        public string Recipient { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}
