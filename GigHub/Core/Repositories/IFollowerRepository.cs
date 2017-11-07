namespace GigHub.Repositories
{
    public interface IFollowerRepository
    {
        bool CheckIfUserIsFollowing(string artistId, string userId);
    }
}