using GigHub.Core;
using GigHub.Core.Dto;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    //[Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var notifications = _unitOfWork.Notifications.GetNotificationsOfAUser(User.Identity.GetUserId());

            return notifications.Select(n => new NotificationDto() {
                 DateTime = n.DateTime,
                 Gig = new GigDto
                 {
                     Artist = new UserDto
                     {
                         Id = n.Gig.Artist.Id,
                         Name = n.Gig.Artist.Name
                     },
                     DateTime = n.Gig.DateTime,
                     ID = n.Gig.ID,
                     IsCanceled = n.Gig.IsCanceled,
                     Venue = n.Gig.Venue
                 },
                 OriginalDateTime = n.OriginalDateTime,
                 OriginalVenue = n.OriginalVenue,
                 Type = n.Type
             });

            //return notifications;
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var notifications = _unitOfWork.Notifications.GetNotificationsNotRead(User.Identity.GetUserId());

            notifications.ToList().ForEach(n => n.Read());
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
