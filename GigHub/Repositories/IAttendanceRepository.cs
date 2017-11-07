using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IAttendanceRepository
    {
        bool CheckIfUserIsAttending(int gigId, string userId);
        IEnumerable<Attendance> GetFutureAttendances(string userId);
    }
}