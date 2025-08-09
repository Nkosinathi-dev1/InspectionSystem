using NotificationService.Contracts.Requests;
using NotificationService.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationResponse> SendNotificationAsync(SendNotificationRequest request);
    }
}
