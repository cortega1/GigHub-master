using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<UserNotification> GetNotificationsNotRead(string userId);
        IEnumerable<Notification> GetNotificationsOfAUser(string userId);
    }
}