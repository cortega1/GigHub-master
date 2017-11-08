using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;
        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetMyUpcommingGigs(string userId)
        {
            return _context.Gigs
                .Where(g =>
                    g.ArtistId == userId &&
                    g.DateTime > DateTime.Now &&
                    !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigWithAttendees(int id)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.ID == id);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGig(int id)
        {
            return _context.Gigs.Single(g => g.ID == id);
        }

        public Gig GetGigDetails(int id)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.ID == id && !g.IsCanceled);
        }

        public Gig GetGigWithAttendees(int gigId, string userId)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.ID == gigId && g.ArtistId == userId);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}