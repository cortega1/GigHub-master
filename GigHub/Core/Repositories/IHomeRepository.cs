using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IHomeRepository
    {
        ILookup<int, Attendance> GetGigsUserGoing(string userId);
        IEnumerable<Gig> GetUpcommingGigs();
    }
}