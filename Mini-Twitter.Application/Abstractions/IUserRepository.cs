namespace Mini_Twitter.Application.Abstractions
{
    public interface IUserRepository : IAsyncRepository<ApplicationUser>
    {
        #region GET
        IQueryable<Tweet> GetTweetsForUser(string userId);
        #endregion

        #region POST
        Task<Retweet?> AddRetweetForUserAsync(string userId, int tweetId);
        #endregion
    }
}
