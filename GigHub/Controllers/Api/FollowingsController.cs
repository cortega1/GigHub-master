using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            if (_unitOfWork.Followers.CheckIfUserIsFollowing(dto.FolloweeId, userId))
            {
                return BadRequest("Following already exists.");
            }

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _unitOfWork.Followers.Add(following);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow (string id)
        {
            var following = _unitOfWork.Followers.GetFollow(User.Identity.GetUserId(), id);

            if (following == null)
            {
                return NotFound();
            }

            _unitOfWork.Followers.Remove(following);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
