namespace Mini_Twitter.Infrastructure.Repositories
{
    public class TweetRepository : BaseRepository<Tweet>, ITweetRepository
    {
        #region Construcotrs
        public TweetRepository(TwitterContext context) : base(context) { }
        #endregion

        #region POST
        public async Task<Tweet> AddTweetAsync(Tweet tweet)
        {
            try
            {
                _context.Tweets.Add(tweet);
                await _context.SaveChangesAsync();

                return tweet;
            }
            catch (Exception ex) when (ex is ArgumentNullException
                                    || ex is InvalidOperationException
                                    || ex is DbUpdateException
                                    || ex is PostgresException)
            {
                throw new DataFailureException(ex.Message);
            }
        }
        #endregion

        #region PUT
        public async Task<Tweet?> UpdateTweetAsync(int id, Tweet tweet)
        {
            try
            {
                var currentTweet = await GetByIdAsync(id);

                if (currentTweet == null)
                    return null;

                currentTweet.Context = tweet.Context;
                _context.Update(currentTweet);
                await _context.SaveChangesAsync();

                return currentTweet;
            }
            catch (Exception ex) when (ex is ArgumentNullException
                                    || ex is InvalidOperationException
                                    || ex is DbUpdateException
                                    || ex is PostgresException)
            {
                throw new DataFailureException(ex.Message);
            }
        }
        #endregion

        #region DELETE
        public async Task<bool> RemoveTweetAsync(int id)
        {
            try
            {
                var tweet = await GetByIdAsync(id);

                if (tweet == null)
                    return false;

                if (tweet.IsDeleted == true)
                    return true;

                tweet.IsDeleted = true;
                tweet.LastModifiedDate = DateTime.UtcNow;
                _context.Update(tweet);

                return _context.SaveChanges() > 0;
            }
            catch (Exception ex) when (ex is ArgumentNullException
                                    || ex is InvalidOperationException
                                    || ex is DbUpdateException
                                    || ex is PostgresException)
            {
                throw new DataFailureException(ex.Message);
            }
        }
        #endregion
    }
}
