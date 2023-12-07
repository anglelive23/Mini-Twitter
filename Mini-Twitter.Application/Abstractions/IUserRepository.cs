namespace Mini_Twitter.Application.Abstractions
{
    public interface IUserRepository : IAsyncRepository<ApplicationUser>
    {
        #region GET
        IQueryable<Tweet> GetTweetsForUser(string userId);
        IQueryable<Retweet> GetRetweetsForUser(string userId);
        IQueryable<Retweet> GetRetweetByIdForUser(string userId, int retweetId);
        #endregion

        #region POST
        Task<Retweet?> AddRetweetForUserAsync(string userId, int tweetId);
        #endregion

        #region DELETE
        Task<bool> DeleteUserAsync(string userId);
        #endregion
    }
}
