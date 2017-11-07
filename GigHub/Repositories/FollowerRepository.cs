using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Repositories
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
    }
}