using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IFollowerRepository
    {
        bool CheckIfUserIsFollowing(string artistId, string userId);
        List<ApplicationUser> GetListOfFollowees(string userId);
        Following GetFollow(string userId, string FolloweeId);
        void Add(Following following);
        void Remove(Following following);
    }
}