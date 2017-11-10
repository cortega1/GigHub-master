using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Core.Models
{
    public class UserNotification
    {
        [Key]
        public string UserId { get; private set; }

        [Key]
        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; set; }

        UserNotification()
        {
        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("User");
            if (notification == null)
                throw new ArgumentNullException("notification");

            User = user;
            Notification = notification;
        }

        public void Read()
        {
            this.IsRead = true;
        }
    }
}