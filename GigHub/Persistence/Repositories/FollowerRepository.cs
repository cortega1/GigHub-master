using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Persistence.Repositories
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CheckIfUserIsFollowing(string artistId, string userId)
        {
            return _context.Followings
                    .Any(f => f.FolloweeId == artistId && f.FollowerId == userId);
        }

        public List<ApplicationUser> GetListOfFollowees(string userId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee).ToList();
        }

        public Following GetFollow(string userId, string FolloweeId)
        {
            return _context.Followings
                .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == FolloweeId);
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}