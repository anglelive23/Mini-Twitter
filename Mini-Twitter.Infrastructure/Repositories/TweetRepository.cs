using Microsoft.AspNetCore.OData.Deltas;

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

        public async Task<Reply?> AddReplyForTweetAsync(int tweetId, Reply reply)
        {
            try
            {
                var tweet = await GetByIdAsync(tweetId);

                if (tweet == null)
                    return null;

                reply.TweetId = tweetId;
                _context.Replies.Add(reply);
                await _context.SaveChangesAsync();

                return reply;
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

        public async Task<Reply?> UpdateReplyForTweetAsync(int tweetId, int replyId, Reply reply)
        {
            try
            {
                if (!await IsExistingTweet(tweetId))
                    return null;

                var currentReply = await _context
                    .Replies
                    .FirstOrDefaultAsync(a => a.Id == replyId && a.TweetId == tweetId && a.IsDeleted == false);

                if (currentReply is null)
                    return null;

                currentReply.Context = reply.Context;
                _context.Update(currentReply);
                await _context.SaveChangesAsync();
                return currentReply;
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

        #region PATCH
        public async Task<Tweet?> PartUpdateTweetAsync(int id, Delta<Tweet> tweet)
        {
            try
            {
                var currentTweet = await GetByIdAsync(id);

                if (currentTweet == null)
                    return null;

                tweet.Patch(currentTweet);
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
                await _context.Replies
                    .Where(r => r.TweetId == id)
                    .ExecuteUpdateAsync(u => u.SetProperty(p => p.IsDeleted, true));
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

        public async Task<bool> RemoveReplyForTweetAsync(int tweetId, int replyId)
        {
            try
            {
                var reply = await _context.Replies
                    .AnyAsync(a => a.Id == replyId && a.TweetId == tweetId && a.IsDeleted == false);

                if (reply is false)
                    return false;

                _context.Replies
                    .Where(s => s.Id == replyId && s.TweetId == tweetId && s.IsDeleted == false)
                    .ExecuteUpdate(p => p.SetProperty(p => p.IsDeleted, true));
                return true;
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

        #region Helper Methods
        public async Task<bool> IsExistingTweet(int tweetId)
        {
            return await _context
                .Tweets
                .AnyAsync(t => t.Id == tweetId && t.IsDeleted == false);
        }
        #endregion
    }
}
