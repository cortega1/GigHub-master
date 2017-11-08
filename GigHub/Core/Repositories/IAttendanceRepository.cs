using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        bool CheckIfUserIsAttending(int gigId, string userId);
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        Attendance GetAttendanceOfAnUserToAGig(int gigId, string userId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}