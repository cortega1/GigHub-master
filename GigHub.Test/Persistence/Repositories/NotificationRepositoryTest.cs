using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Test.Persistence.Repositories
{
    [TestClass]
    public class NotificationRepositoryTest
    {
        private NotificationRepository _repository;
        private Mock<DbSet<UserNotification>> _mockNotifications;
        private Mock<DbSet<Gig>> _mockGigs;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockNotifications = new Mock<DbSet<UserNotification>>();
            _mockGigs = new Mock<DbSet<Gig>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            mockContext.SetupGet(c => c.UserNotifications).Returns(_mockNotifications.Object);

            _repository = new NotificationRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetNewNotificationsFor_NotificationIsRead_ShouldNotBeReturned()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);
            userNotification.Read();

            _mockNotifications.SetSourse(new[] { userNotification });

            var notifications = _repository.GetNotificationsNotRead(user.Id);

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotificationsFor_NotificationIsForADifferentUser_ShouldNotBeReturned()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockNotifications.SetSourse(new[] { userNotification });

            var notifications = _repository.GetNotificationsNotRead(user.Id + "-");

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotificationsFor_NewNotificationForTheGivenUser_ShouldBeReturned()
        {
            var gig = new Gig { ID=1, GenreId=1, DateTime=DateTime.Now.AddDays(1), ArtistId="1" };

            var notification = Notification.GigCanceled(gig);
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockNotifications.SetSourse(new[] { userNotification });

            var notifications = _repository.GetNotificationsNotRead(user.Id);

            notifications.Should().HaveCount(1);
            notifications.First().Should().Be(userNotification);
        }
    }
}
