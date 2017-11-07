using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGig(int id);
        Gig GetGigDetails(int id);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttendees(int id);
        IEnumerable<Gig> GetMyUpcommingGigs(string userId);
    }
}