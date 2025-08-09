using Microsoft.EntityFrameworkCore;
using NotificationService.Application.Interfaces;
using NotificationService.Contracts.Requests;
using NotificationService.Contracts.Responses;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Enums;
using NotificationService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Services
{
    //cheat fix I call NotificationService -> NotificationServiceImpl
    public class NotificationServiceImpl : INotificationService
    {
        private readonly INotificationRepository _repo;
        public NotificationServiceImpl(INotificationRepository repo) => _repo = repo;

        public async Task<NotificationResponse> SendNotificationAsync(SendNotificationRequest request)
        {
            var notification = new Notification
            {
                Recipient = request.Recipient,
                Subject = request.Subject,
                Message = request.Message,
                Type = Enum.Parse<NotificationType>(request.Type, true),
                IsSent = true // TODO I need to see the provider to use to send via email/SMS
            };

            await _repo.AddAsync(notification);
            await _repo.SaveChangesAsync();

            return new NotificationResponse
            {
                Id = notification.Id,
                Success = true
            };
        }
    }
}
