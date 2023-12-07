namespace Mini_Twitter.Application.Abstractions
{
    public interface ITweetRepository : IAsyncRepository<Tweet>
    {
        #region POST
        Task<Tweet> AddTweetAsync(Tweet tweet);
        Task<Reply?> AddReplyForTweetAsync(int tweetId, Reply reply);
        #endregion

        #region PUT
        Task<Tweet?> UpdateTweetAsync(int id, Tweet tweet);
        Task<Reply?> UpdateReplyForTweetAsync(int tweetId, int replyId, Reply reply);
        #endregion

        #region PATCH
        Task<Tweet?> PartUpdateTweetAsync(int id, Delta<Tweet> tweet);
        #endregion

        #region DELETE
        Task<bool> RemoveTweetAsync(int id);
        Task<bool> RemoveReplyForTweetAsync(int tweetId, int replyId);
        #endregion
    }
}
