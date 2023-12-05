namespace Mini_Twitter.Application.Abstractions
{
    public interface IFollowService
    {
        Task<bool> FollowUserAsync(string followerId, string followeeId); // me, the one i will follow
        Task<bool> UnfollowUserAsync(string followerId, string followeeId); // me, the one i will unfoloow
    }
}
