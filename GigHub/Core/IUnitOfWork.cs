using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowerRepository Followers { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }
        IHomeRepository Home { get; }
        INotificationRepository Notifications { get; }

        void Complete();
    }
}