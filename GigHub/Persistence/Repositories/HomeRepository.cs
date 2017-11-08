using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetUpcommingGigs()
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now);
        }

        public ILookup<int, Attendance> GetGigsUserGoing(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.GigId);
        }
    }
}