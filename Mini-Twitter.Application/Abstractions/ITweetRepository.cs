namespace Mini_Twitter.Application.Abstractions
{
    public interface ITweetRepository : IAsyncRepository<Tweet>
    {
        #region POST
        Task<Tweet> AddTweetAsync(Tweet tweet);
        #endregion

        #region PUT
        Task<Tweet?> UpdateTweetAsync(int id, Tweet tweet);
        #endregion

        #region DELETE
        Task<bool> RemoveTweetAsync(int id);
        #endregion
    }
}
