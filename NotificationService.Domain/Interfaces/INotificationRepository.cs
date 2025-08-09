using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Interfaces
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task SaveChangesAsync();
    }
}
